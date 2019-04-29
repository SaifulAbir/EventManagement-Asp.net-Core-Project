using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagement.Filters;
using EventManagement.Interface;
using EventManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventManagement.Controllers
{   [ValidateUserSession]
    public class UserProfileController : Controller
    {
        IRegistration _IRepository;
        public UserProfileController(IRegistration IRepository)
        {
            _IRepository = IRepository;
        }

        [HttpGet]
        public IActionResult Profile()
        {
            try
            {
                var profile = _IRepository.Userinformation(Convert.ToInt32(HttpContext.Session.GetString("UserID")));
                return View(profile);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult Edit()
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserID")))
                {
                    return RedirectToAction("Profile", "UserProfile");
                }

                Registration UserEdit = _IRepository.GetUserByID(Convert.ToInt32(HttpContext.Session.GetString("UserID")));

                return View("Edit", UserEdit);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Registration Registration)
        {


            Registration objUser = new Registration
            {

                ID = Registration.ID,
                Name = Registration.Name,
                Address = Registration.Address,
                EmailID = Registration.EmailID,
                Country = Registration.Country,
                State = Registration.State,
                City = Registration.City,
                Birthdate = Registration.Birthdate,
                Gender = Registration.Gender,
                Mobileno = Registration.Mobileno
            };

            _IRepository.UpdateUser(objUser);

            TempData["UpdateMessage"] = "Profile Updated Successfully";
            ModelState.Clear();
            return View(new Registration());



        }

    }
}
