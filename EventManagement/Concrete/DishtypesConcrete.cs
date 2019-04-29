using EventManagement.Interface;
using EventManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Concrete
{
    public class DishtypesConcrete : IDishtypes
    {
        private DatabaseContext _context;

        public DishtypesConcrete(DatabaseContext context)
        {
            _context = context;
        }

        public List<Dishtypes> GetDishtypeList()
        {
            try
            {
                return _context.Dishtypes.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
