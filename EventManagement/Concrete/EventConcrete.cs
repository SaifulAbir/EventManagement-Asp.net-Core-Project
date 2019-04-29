using EventManagement.Interface;
using EventManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Concrete
{
    public class EventConcrete : IEvent
    {
        private DatabaseContext _context;

        public EventConcrete(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return _context.Event.ToList();
        }
    }
}
