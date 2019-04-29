using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagement.Interface;
using EventManagement.Library;
using EventManagement.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventManagement.Controllers
{
    public class RegistrationController : Controller
    {

        IRegistration _IRepository;
        IRoles _IRoles;
        public RegistrationController(IRegistration IRepository, IRoles IRoles)
        {
            _IRepository = IRepository;
            _IRoles = IRoles;
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Registration()
        {
            try
            {
                Registration Registration = new Registration();
                Registration.Country = null;
                Registration.City = null;
                Registration.State = null;
                return View(Registration);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registration(Registration Registration)
        {
            try
            {
                var isUsernameExists = _IRepository.CheckUserNameExists(Registration.Username);

                if (isUsernameExists)
                {
                    ModelState.AddModelError("", errorMessage: "Username Already Used try unique one!");
                }
                else
                {
                    Registration.CreatedOn = DateTime.Now;
                    Registration.RoleID = _IRoles.getRolesofUserbyRolename("Users");
                    Registration.Password = EncryptionLibrary.EncryptText(Registration.Password);
                    Registration.ConfirmPassword = EncryptionLibrary.EncryptText(Registration.ConfirmPassword);
                    if (_IRepository.AddUser(Registration) > 0)
                    {
                        TempData["MessageRegistration"] = "You have successfully registered!";
                        ModelState.Clear();
                        return View(new Registration());
                    }
                    else
                    {
                        return View(Registration);
                    }
                }

                return View(Registration);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public JsonResult CheckUserNameExists(string Username)
        {
            try
            {
                var isUsernameExists = _IRepository.CheckUserNameExists(Username);
                if (isUsernameExists)
                {
                    return Json(data: true);
                }
                else
                {
                    return Json(data: false);
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
