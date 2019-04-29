using EventManagement.Interface;
using EventManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Concrete
{
    public class CountryConcrete : ICountry
    {
        private DatabaseContext _context;

        public CountryConcrete(DatabaseContext context)
        {
            _context = context;
        }

        public List<Country> ListofCountry()
        {
            return _context.Country.ToList();
        }
    }
}
