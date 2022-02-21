using APIGateway.Authorization.Helpers;
using APIGateway.Authorization.Requirements.Booking;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGateway.Authorization.Handlers.Booking
{
    //necessary for implementation of resource-based authorization
    //checks if get bookings by guest requirement is met
    //applies to retrieval of bookings by guest id
    public class GetBookingsByGuestAuthorizationHandler
        : AuthorizationHandler<GetBookingsByGuestRequirement, long>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            GetBookingsByGuestRequirement requirement,
            long resource)
        {
            //if id of logged user and id of guest for whom bookings are retrieved are the same
            //then user can view bookings
            if (AuthorizationHelper.GetLoggedUserId(context.User) == resource)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
