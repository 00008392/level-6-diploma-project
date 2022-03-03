using FrontEndApp.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<LoginResponse> LogIn(LoginRequest user);
        Task LogOut(Action onLogooutAction = null);
        Task<bool> IsAuthenticated();
        Task<long?> GetLoggedUserId();
    }
}
