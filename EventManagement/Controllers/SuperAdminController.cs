using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagement.Interface;
using EventManagement.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventManagement.Controllers
{
    
    public class SuperAdminController : Controller
    {
        IContact _IContact;
        public SuperAdminController(IContact IContact)
        {
            _IContact = IContact;
        }
        // GET: /<controller>/
        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Index()
        {
            SetSlider();
            return View();
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

        [NonAction]
        private void SetSlider()
        {
            var Images = (from images in _IContact.ShowAllFeedback()
                          select new Feedback { FeedFilename = images.FeedFilename, FeedFilePath = images.FeedFilePath, Comment = images.Comment, Profession = images.Profession, Createdby = images.Createdby }).ToList();


            ViewBag.SliderImages = Images;
        }
    }
}
