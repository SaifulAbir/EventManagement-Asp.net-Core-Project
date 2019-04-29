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
    public class EventDataController : Controller
    {
        private IEvent _IEvent;
        public EventDataController(IEvent IEvent)
        {
            _IEvent = IEvent;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Event> Get()
        {
            try
            {
                return _IEvent.GetAllEvents();
            }
            catch (Exception)
            {
                throw;
            }
        }



    }
}
