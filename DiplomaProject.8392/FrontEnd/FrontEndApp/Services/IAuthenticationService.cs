using FrontEndApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Services
{
    public interface IAuthenticationService
    {
        Task<LoggedUser> LogIn(Login user);
        Task LogOut();
        Task<bool> IsAuthenticated();
    }
}
