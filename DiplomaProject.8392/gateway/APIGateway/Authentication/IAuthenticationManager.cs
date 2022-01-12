using Account.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGateway.Authentication
{
   public interface IAuthenticationManager
    {
        Task<LoginReply> AuthenticateAsync(LoginRequest request);
    }
}
