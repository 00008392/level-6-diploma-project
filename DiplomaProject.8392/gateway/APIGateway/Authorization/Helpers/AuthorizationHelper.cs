using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace APIGateway.Authorization.Helpers
{
    public static class AuthorizationHelper
    {
        //gets the id of logged in user from provided token
        public static long? GetLoggedUserId(ClaimsPrincipal user)
        {
           if(user.FindFirst(ClaimTypes.NameIdentifier)==null)
            {
                return null;
            }
            return long.Parse(user.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}
