using APIGateway.Authorization.Helpers;
using APIGateway.Authorization.Requirements.Booking;
using Booking.API;
using Microsoft.AspNetCore.Authorization;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGateway.Authorization.Handlers.Booking
{
    //necessary for implementation of resource-based authorization
    //checks if delete booking requirement is met
    public class DeleteBookingAuthorizationHandler :
        AuthorizationHandler<DeleteBookingRequirement, long>
    {
        //inject grpc booking service to retrieve booking info
        private BookingService.BookingServiceClient _bookingClient;

        public DeleteBookingAuthorizationHandler(BookingService.BookingServiceClient bookingClient)
        {
            _bookingClient = bookingClient;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            DeleteBookingRequirement requirement,
            long resource)
        {
            //only person who created booking can delete it 
            //if id of guest and id of logged user are the same then user can delete booking
            var booking = await _bookingClient.GetBookingDetailsAsync(new Request { Id = resource });
            if (AuthorizationHelper.GetLoggedUserId(context.User) == booking.GuestId)
            {
                context.Succeed(requirement);
            }
        }
    }
}
