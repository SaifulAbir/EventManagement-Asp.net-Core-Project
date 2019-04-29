using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EventManagement.Concrete;
using EventManagement.Filters;
using EventManagement.Interface;
using EventManagement.Library;
using EventManagement.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventManagement.Controllers
{
    [ValidateUserSession]
    public class CustomerController : Controller
    {
        ILogin _ILogin;
        IRegistration _IRegistration;
        IContact _IContact;
        DatabaseContext _context;
        private ITotalbilling _ITotalbilling;
        private readonly IHostingEnvironment _environment;
        public CustomerController(ILogin ILogin, IRegistration IRegistration, IContact IContact, IHostingEnvironment IHostingEnvironment, DatabaseContext context, ITotalbilling ITotalbilling)
        {
            _ILogin = ILogin;
            _IRegistration = IRegistration;
            _IContact = IContact;
            _environment = IHostingEnvironment;
            _context = context;
            _ITotalbilling = ITotalbilling;
        }
        // GET: /<controller>/
        public IActionResult Dashboard()
        {
            var bookingOverview = _ITotalbilling.GetOverview(Convert.ToInt32(HttpContext.Session.GetString("UserID")));
            return View(bookingOverview);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View(new ChangePasswordModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(ChangePasswordModel ChangePasswordModel)
        {
            if (!ModelState.IsValid)
            {
                return View(ChangePasswordModel);
            }

            var password = EncryptionLibrary.EncryptText(ChangePasswordModel.Password);
            var registrationModel = _IRegistration.Userinformation(Convert.ToInt32(HttpContext.Session.GetString("UserID")));

            if (registrationModel.Password == password)
            {
                var registration = new Registration();
                registration.Password = EncryptionLibrary.EncryptText(ChangePasswordModel.NewPassword);
                registration.ID = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
                var result = _ILogin.UpdatePassword(registration);

                if (result)
                {
                    TempData["MessageUpdate"] = "Password Updated Successfully";
                    ModelState.Clear();
                    return View(new ChangePasswordModel());
                }
                else
                {
                    return View(ChangePasswordModel);
                }
            }
            else
            {
                TempData["MessageUpdate"] = "Invalid Password";
                return View(ChangePasswordModel);
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
        public IActionResult Index()
        {
            SetSlider();
            return View();
        }
        [HttpGet]
        public IActionResult Feedback()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Feedback(Feedback Feedback)
        {
            var newFileName = string.Empty;

            if (HttpContext.Request.Form.Files != null)
            {
                var fileName = string.Empty;
                string PathDB = string.Empty;

                var files = HttpContext.Request.Form.Files;

                if (files == null)
                {
                    ModelState.AddModelError("", "Upload Your Photo !");
                    return View();
                }

                if (!ModelState.IsValid)
                {
                    return View("Feedback");
                }

                var uploads = Path.Combine(_environment.WebRootPath, "FeedbackImages");

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                        var FileExtension = Path.GetExtension(fileName);
                        newFileName = myUniqueFileName + FileExtension;
                        fileName = Path.Combine(_environment.WebRootPath, "FeedbackImages") + $@"\{newFileName}";
                        PathDB = "FeedbackImages/" + newFileName;
                        using (FileStream fs = System.IO.File.Create(fileName))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                    }
                }
                var name = (from user in _context.Registration
                            where user.ID == Convert.ToInt32(HttpContext.Session.GetString("UserID"))
                            select user.Name).SingleOrDefault();
                Feedback objEqu = new Feedback
                {
                    FeedFilename = newFileName,
                    FeedFilePath = PathDB,
                    FeedbackID = 0,
                    Experience = Feedback.Experience,
                    Comment = Feedback.Comment,
                    Createdby = name,
                    Profession = Feedback.Profession
                };

                _IContact.SaveFeedback(objEqu);

                TempData["FeedbackMessage"] = "Thank you for your Feedback!";
                ModelState.Clear();
                return View(new Feedback());

            }
            return View(Feedback);
        }
        [NonAction]
        private void SetSlider()
        {
            var Images = (from images in _IContact.ShowAllFeedback()
                          select new Feedback { FeedFilename = images.FeedFilename, FeedFilePath = images.FeedFilePath, Comment = images.Comment, Profession = images.Profession, Createdby = images.Createdby}).ToList();


            ViewBag.SliderImages = Images;
        }
    }
}
