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
    public class StateAPIController : Controller
    {
        private IState _IState;

        public StateAPIController(IState IState)
        {
            _IState = IState;
        }

        // POST api/values
        [HttpPost]
        public List<States> Post(string id)
        {
            try
            {
                if (id == null)
                {
                    return null;
                }

                var listofState = _IState.ListofState(Convert.ToInt32(id));
                return listofState;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
