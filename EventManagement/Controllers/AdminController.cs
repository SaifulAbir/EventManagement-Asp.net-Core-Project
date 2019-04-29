using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagement.Concrete;
using EventManagement.Filters;
using EventManagement.Interface;
using EventManagement.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventManagement.Controllers
{
    [ValidateAdminSession]
    public class AdminController : Controller
    {
        IContact _IContact;
        ITotalbilling _ITotalbilling;
        public AdminController(IContact IContact, ITotalbilling ITotalbilling)
        {
            _IContact = IContact;
            _ITotalbilling = ITotalbilling;
        }
        // GET: /<controller>/
        public IActionResult Dashboard()
        {
            var AdminOverview = _ITotalbilling.GetAdminOverview();
            return View(AdminOverview);
        }
        public IActionResult Index()
        {
            SetSlider();
            return View();
        }
        public IActionResult ViewAllFeedback()
        {
            return View();
        }

        public IActionResult LoadFeedbackData()
        {
            try
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

                var v = _IContact.ShowFeedback(sortColumn, sortColumnDir, searchValue);
                recordsTotal = v.Count();
                var data = v.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }

        }
        [HttpGet]
        public IActionResult Comment(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return RedirectToAction("Admin", "ViewAllFeedback");
                }

                Feedback CommentDetails = _IContact.FeedbackByID(Convert.ToInt32(id));

                return View("Comment", CommentDetails);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IActionResult ViewAllMessage()
        {
            return View();
        }

        public IActionResult LoadMessageData()
        {
            try
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

                var v = _IContact.ShowMessage(sortColumn, sortColumnDir, searchValue);
                recordsTotal = v.Count();
                var data = v.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }

        }
        [HttpGet]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            string markers = "[";

            markers += "{";
            markers += string.Format("'title': '{0}',", "Uttara");
            markers += string.Format("'lat': '{0}',", 23.8641688);
            markers += string.Format("'lng': '{0}',", 90.39912959999992);
            markers += string.Format("'description': '{0}'", "Uttara");
            markers += "},";

            markers += "{";
            markers += string.Format("'title': '{0}',", "Dhaka");
            markers += string.Format("'lat': '{0}',", 23.8641688);
            markers += string.Format("'lng': '{0}',", 90.39912959999992);
            markers += string.Format("'description': '{0}'", "DHaka");
            markers += "},";

            markers += "];";
            ViewBag.Markers = markers;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(Contact Contact)
        {
            string markers = "[";

            markers += "{";
            markers += string.Format("'title': '{0}',", "Uttara");
            markers += string.Format("'lat': '{0}',", 23.8641688);
            markers += string.Format("'lng': '{0}',", 90.39912959999992);
            markers += string.Format("'description': '{0}'", "Uttara");
            markers += "},";

            markers += "{";
            markers += string.Format("'title': '{0}',", "Dhaka");
            markers += string.Format("'lat': '{0}',", 23.8641688);
            markers += string.Format("'lng': '{0}',", 90.39912959999992);
            markers += string.Format("'description': '{0}'", "DHaka");
            markers += "},";

            markers += "];";
            ViewBag.Markers = markers;

            try
            {
                if (_IContact.AddMessage(Contact) > 0)
                {
                    TempData["MessageContact"] = "Message Sent Successfully!";
                    ModelState.Clear();
                    return View(new Contact());
                }
                else
                {
                    return View(Contact);
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult Message(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return RedirectToAction("Admin", "ViewAllMessage");
                }

                Contact MessageDetails = _IContact.MessageByID(Convert.ToInt32(id));

                return View("Message", MessageDetails);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [NonAction]
        private void SetSlider()
        {
            var Images = (from images in _IContact.ShowAllFeedback()
                          select new Feedback { FeedFilename = images.FeedFilename, FeedFilePath = images.FeedFilePath, Comment = images.Comment, Createdby = images.Createdby, Profession = images.Profession }).ToList();


            ViewBag.SliderImages = Images;
        }
    }
}
