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
    [Route("api/[controller]")]
    public class CountryAPIController : Controller
    {
        private ICountry _ICountry;

        public CountryAPIController(ICountry ICountry)
        {
            _ICountry = ICountry;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Country> Get()
        {
            try
            {
                var listofCountry = _ICountry.ListofCountry();
                listofCountry.Insert(0, new Country { CountryID = 0, Name = "----Select----" });
                return listofCountry;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
