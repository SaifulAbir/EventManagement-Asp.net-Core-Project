using EventManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Interface
{
    public interface ILogin
    {
        Registration ValidateUser(string userName, string passWord);
        bool UpdatePassword(Registration Registration);
    }
}
