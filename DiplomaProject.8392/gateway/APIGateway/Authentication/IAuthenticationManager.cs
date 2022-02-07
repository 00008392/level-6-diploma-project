using Account.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGateway.Authentication
{
    //service for user authentication
   public interface IAuthenticationManager
    {
        Task<LoginResponse> AuthenticateAsync(LoginRequest request);
    }
}
