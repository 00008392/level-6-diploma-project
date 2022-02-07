using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGateway.Authorization.Requirements.User
{
    //necessary for implementation of resource-based authorization
    //applies to update user and update password actions
    public class UserUpdateRequirement: IAuthorizationRequirement
    {
    }
}
