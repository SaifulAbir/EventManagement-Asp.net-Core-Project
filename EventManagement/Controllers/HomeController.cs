using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EventManagement.Models;
using EventManagement.Interface;

namespace EventManagement.Controllers
{
    public class HomeController : Controller
    {
        private IVenue _IVenue;
        private IEquipment _IEquipment;
        private IContact _IContact;
        public HomeController(IVenue IVenue, IEquipment IEquipment, IContact IContact)
        {
            _IVenue = IVenue;
            _IEquipment = IEquipment;
            _IContact = IContact;
        }

        public IActionResult Index()
        {
            SetSlider();
            return View();
        }

        [NonAction]
        private void SetSlider()
        {
            var Images = (from images in _IContact.ShowAllFeedback()
                          select new Feedback { FeedFilename = images.FeedFilename, FeedFilePath = images.FeedFilePath, Comment = images.Comment, Createdby = images.Createdby, Profession = images.Profession }).ToList();


            ViewBag.SliderImages = Images;
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}
