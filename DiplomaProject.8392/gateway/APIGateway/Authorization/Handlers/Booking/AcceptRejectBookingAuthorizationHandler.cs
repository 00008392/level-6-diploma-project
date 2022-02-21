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
    //checks if accept/reject booking requirement is met
    public class AcceptRejectBookingAuthorizationHandler :
        AuthorizationHandler<AcceptRejectBookingRequirement, long>
    {
        //inject grpc booking service to retrieve booking info
        private BookingService.BookingServiceClient _bookingClient;

        public AcceptRejectBookingAuthorizationHandler(BookingService.BookingServiceClient bookingClient)
        {
            _bookingClient = bookingClient;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            AcceptRejectBookingRequirement requirement,
            long resource)
        {
            //only owner of booked accommodation can accept or reject booking
            //if id of post owner and id of logged user are the same then user
            //can change booking status
            var booking = await _bookingClient.GetBookingDetailsAsync(new Request { Id = resource });
            if (AuthorizationHelper.GetLoggedUserId(context.User) == booking.Post?.OwnerId)
            {
                context.Succeed(requirement);
            }
        }
    }
}
