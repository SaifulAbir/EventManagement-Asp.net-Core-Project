using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagement.Interface;
using EventManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using EventManagement.Concrete;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventManagement.Controllers
{
    public class ShowBookingDetailsController : Controller
    {
        private IBookingVenue _IBookingVenue;
        private IVenue _IVenue;
        private ITotalbilling _ITotalbilling;
        private DatabaseContext _context;

        public ShowBookingDetailsController(IBookingVenue IBookingVenue, IVenue IVenue, ITotalbilling ITotalbilling, DatabaseContext context)
        {
            _IBookingVenue = IBookingVenue;
            _IVenue = IVenue;
            _ITotalbilling = ITotalbilling;
            _context = context;
        }


        [HttpGet]
        public IActionResult AllBookings()
        {
            return View();
        }


        public JsonResult LoadAllBookings()
        {
            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDir = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            if (!string.IsNullOrEmpty(Convert.ToString(HttpContext.Session.GetString("UserID"))))
            {
                var result = _IBookingVenue.ShowBookingDetail();

                if (result.Count() > 0)
                {
                    var v = _IBookingVenue.ShowAllBooking(sortColumn, sortColumnDir, searchValue);
                    recordsTotal = v.Count();
                    var data = v.Skip(skip).Take(pageSize).ToList();
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
                }
                else
                {
                    return Json("Failed");
                }
            }
            else
            {
                return Json("Failed");
            }
        }


        [HttpGet]
        public IActionResult BookingApproval(string BookingNo)
        {

            try
            {
                if (string.IsNullOrEmpty(BookingNo))
                {
                    return RedirectToAction("AllBookings", "ShowBookingDetails");
                }

                var details = _ITotalbilling.GetBillingDetailsbyBookingNo(BookingNo);
                return View(details);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BookingApproval(BillingModel BillingModel, string submit)
        {
            var BookingUser = (from BD in _context.BookingDetails
                             join uid in _context.Registration on BD.Createdby equals uid.ID
                             where BD.BookingNo == BillingModel.BookingNo
                             select new { BD.BookingNo, BD.BookingDate, BD.BookingID, uid.Name, uid.EmailID }).Single();
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("Dream Weeding", "saifulislamlk@gmail.com"));
            message.To.Add(new MailboxAddress(BookingUser.Name, BookingUser.EmailID));
            //message.Subject = "Event Booking Approved";
            //message.Body = new TextPart("plain")
            //{
            //    Text = "Your Booking Application is Approved!"
            //};
            
            string value = string.Empty;

            if (string.IsNullOrEmpty(BillingModel.BookingNo))
            {
                return RedirectToAction("AllBookings", "ShowBookingDetails");
            }

            if (submit == "Approve")
            {
                message.Subject = "Event Booking Approved";
                message.Body = new TextPart("plain")
                {
                    Text = "Dear "+ BookingUser.Name + ",\n" +"Your Booking Application is Approved!" + "\n\n" + "Thank you for being with us, Dream Weeding!"

                };
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("saifulislamlk@gmail.com", "saiful002528");
                    client.Send(message);
                    client.Disconnect(true);
                }
                value = "A";
            }
            else if (submit == "Reject")
            {
                message.Subject = "Event Booking Rejected!";
                message.Body = new TextPart("plain")
                {
                    Text = "Dear " + BookingUser.Name + ",\n" + "Your Booking Application is Rejected!"+"\n\n"+"Thank you for being with us, Dream Weeding!"
                };
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("saifulislamlk@gmail.com", "saiful002528");
                    client.Send(message);
                    client.Disconnect(true);
                }
                value = "R";
            }
            else
            {
                value = "P";
            }


            var result = _IBookingVenue.UpdateBookingStatus(BillingModel.BookingNo, value);

            if (result > 0)
            {
                if (submit == "Approve")
                {
                    TempData["StatusMessage"] = BillingModel.BookingNo + " has been Approved";
                }
                else if (submit == "Reject")
                {
                    TempData["StatusMessage"] = BillingModel.BookingNo + " has been Cancelled";
                }
                var detailsBooking = _ITotalbilling.GetBillingDetailsbyBookingNo(BillingModel.BookingNo);
                return View(detailsBooking);

            }
            else
            {

                var detailsBooking = _ITotalbilling.GetBillingDetailsbyBookingNo(BillingModel.BookingNo);
                return View(detailsBooking);
            }


        }
    }
}
