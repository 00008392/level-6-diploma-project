using Account.API;
using APIGateway.Authorization.Requirements.User;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using APIGateway.Authorization.Helpers;

namespace APIGateway.Authorization.Handlers.User
{
    //necessary for implementation of resource-based authorization
    //checks if user update requirement is met
    //applies to update user and update password actions
    public class UserUpdateAuthorizationHandler : AuthorizationHandler<UserUpdateRequirement, IUpdateRequest>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            UserUpdateRequirement requirement,
            IUpdateRequest resource)
        {
            //if id of logged user and id of user being modified are the same, then user can access update actions
            if (AuthorizationHelper.GetLoggedUserId(context.User) == resource.Id)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
