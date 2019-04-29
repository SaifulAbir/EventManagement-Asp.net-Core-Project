using EventManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Interface
{
    public interface IRegistration
    {
        int AddUser(Registration entity);
        void AddAdmin(Registration entity);
        bool CheckUserNameExists(string Username);
        RegistrationViewModel Userinformation(int UserID);
        Registration GetUserByID(int id);
        void UpdateUser(Registration Registration);
        IQueryable<RegistrationViewModel> UserinformationList(string sortColumn, string sortColumnDir, string Search);

    }
}
