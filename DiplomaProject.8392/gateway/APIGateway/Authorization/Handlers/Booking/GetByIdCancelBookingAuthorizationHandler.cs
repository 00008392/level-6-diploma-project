using APIGateway.Authorization.Helpers;
using APIGateway.Authorization.Requirements.Booking;
using Booking.API;
using Microsoft.AspNetCore.Authorization;
using PostFeedback.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGateway.Authorization.Handlers.Booking
{
    //necessary for implementation of resource-based authorization
    //checks if get bookings by id/ cancel booking requirement is met
    //applies to retrieval of booking by id and cancellation of booking
    public class GetByIdCancelBookingAuthorizationHandler :
        AuthorizationHandler<GetByIdCancelBookingRequirement, long>
    {
        private readonly BookingService.BookingServiceClient _bookingClient;

        public GetByIdCancelBookingAuthorizationHandler(BookingService.BookingServiceClient bookingClient)
        {
            _bookingClient = bookingClient;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            GetByIdCancelBookingRequirement requirement,
            long resource)
        {
            //only user who created booking (guest) or owner of booked accommodation can access booking by id and cancel it
            //if id of post owner or guest and id of logged user are the same then user can view/cancel bookings
            var userId = AuthorizationHelper.GetLoggedUserId(context.User);
            var booking = await _bookingClient.GetBookingDetailsAsync(new Protos.Common.Request { Id = resource });
            if (userId == booking.GuestId
                ||userId==booking.Post?.OwnerId)
            {
                context.Succeed(requirement);
            }
        }
    }
}
