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
        public static int GetLoggedUserId(ClaimsPrincipal user)
        {
            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}
