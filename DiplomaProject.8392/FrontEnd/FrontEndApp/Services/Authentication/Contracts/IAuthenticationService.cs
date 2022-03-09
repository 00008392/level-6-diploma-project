using FrontEndApp.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Services.Authentication.Contracts
{
    //service that consumes authentication api
    public interface IAuthenticationService
    {
        Task<LoginResponse> LogInAsync(LoginRequest user, Action onLoginAction = null, Action onErrorAction=null);
        Task LogOutAsync(Action onLogoutAction = null);
        Task<bool> IsAuthenticatedAsync();
        Task<long?> GetLoggedUserIdAsync();
    }
}
