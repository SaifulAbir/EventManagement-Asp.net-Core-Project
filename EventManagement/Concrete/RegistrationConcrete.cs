﻿using EventManagement.Interface;
using EventManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Concrete
{
    public class RegistrationConcrete : IRegistration
    {
        private DatabaseContext _context;

        public RegistrationConcrete(DatabaseContext context)
        {
            _context = context;
        }

        public void AddAdmin(Registration entity)
        {
            _context.Registration.Add(entity);
            _context.SaveChanges();
        }

        public int AddUser(Registration entity)
        {
            _context.Registration.Add(entity);
            return _context.SaveChanges();
        }

        public bool CheckUserNameExists(string Username)
        {
            var result = (from user in _context.Registration
                          where user.Username == Username
                          select user).Count();

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public RegistrationViewModel Userinformation(int UserID)
        {
            var result = (from user in _context.Registration
                          join country in _context.Country on user.Country equals country.CountryID
                          join states in _context.States on user.State equals states.StateID
                          join city in _context.City on user.City equals city.CityID
                          where user.ID == UserID
                          select new RegistrationViewModel
                          {
                              CountryName = country.Name,
                              StateName = states.StateName,
                              CityName = city.CityName,
                              Name = user.Name,
                              Address = user.Address,
                              EmailID = user.EmailID,
                              CreatedOn = user.CreatedOn.Value.ToString("dd/MM/yyyy"),
                              Birthdate = user.Birthdate.Value.ToString("dd/MM/yyyy"),
                              Gender = user.Gender == "M" ? "Male" : "Female",
                              Mobileno = user.Mobileno,
                              Username = user.Username,
                              Password = user.Password,
                          }).SingleOrDefault();
            return result;
        }

        public IQueryable<RegistrationViewModel> UserinformationList(string sortColumn, string sortColumnDir, string Search)
        {
            var IQueryableReg = (from user in _context.Registration
                                 join country in _context.Country on user.Country equals country.CountryID
                                 join states in _context.States on user.State equals states.StateID
                                 join city in _context.City on user.City equals city.CityID
                                 where user.RoleID == 2
                                 select new RegistrationViewModel
                                 {
                                     CountryName = country.Name,
                                     StateName = states.StateName,
                                     CityName = city.CityName,
                                     Name = user.Name,
                                     Address = user.Address,
                                     EmailID = user.EmailID,
                                     CreatedOn = user.CreatedOn.Value.ToString("dd/MM/yyyy"),
                                     Birthdate = user.Birthdate.Value.ToString("dd/MM/yyyy"),
                                     Gender = user.Gender == "M" ? "Male" : "Female",
                                     Mobileno = user.Mobileno,
                                     Username = user.Username
                                 });
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                //IQueryableReg = IQueryableReg.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryableReg = IQueryableReg.Where(m => m.Username == Search || m.Mobileno == Search || m.EmailID == Search);
            }

            return IQueryableReg;
        }

        public Registration GetUserByID(int id)
        {
            if (id != 0)
            {
                var uid = _context.Registration.Find(id);
                return uid;
            }
            else
            {
                return new Registration();
            }
        }
        public void UpdateUser(Registration Registration)
        {
            try
            {


                _context.Entry(Registration).Property(x => x.Name).IsModified = true;
                _context.Entry(Registration).Property(x => x.Address).IsModified = true;
                _context.Entry(Registration).Property(x => x.EmailID).IsModified = true;
                _context.Entry(Registration).Property(x => x.Birthdate).IsModified = true;
                _context.Entry(Registration).Property(x => x.Gender).IsModified = true;
                _context.Entry(Registration).Property(x => x.Country).IsModified = true;
                _context.Entry(Registration).Property(x => x.State).IsModified = true;
                _context.Entry(Registration).Property(x => x.City).IsModified = true;
                _context.Entry(Registration).Property(x => x.Mobileno).IsModified = true;
                _context.SaveChanges();


            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
