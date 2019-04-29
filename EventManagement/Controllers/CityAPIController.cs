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
    public class CityAPIController : Controller
    {

        private ICity _ICity;

        public CityAPIController(ICity ICity)
        {
            _ICity = ICity;
        }

        // POST api/values
        [HttpPost]
        public List<City> Post(string id)
        {
            try
            {
                var listofState = _ICity.ListofCity(Convert.ToInt32(id));
                return listofState;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
