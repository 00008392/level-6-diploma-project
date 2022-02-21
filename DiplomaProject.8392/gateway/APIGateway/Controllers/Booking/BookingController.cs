
using APIGateway.Authorization.Helpers;
using APIGateway.Helpers;
using Booking.API;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIGateway.Controllers.Booking
{
    //controller for booking manipulation
    //only authorized access to all actions
    [Authorize]
    [Route("api/bookings")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        //injecting grpc client to access services of booking microservice
        private readonly BookingService.BookingServiceClient _bookingClient;
        //injecting authorization service for resource based authorization
        private readonly IAuthorizationService _authorizationService;

        public BookingController(
            BookingService.BookingServiceClient bookingClient,
            IAuthorizationService authorizationService)
        {
            _bookingClient = bookingClient;
            _authorizationService = authorizationService;
        }

        //retrieve bookings for guest who made booking
        [HttpGet("guest/{guestId}")]
        public async Task<IActionResult> GetBookingsForGuest(long guestId)
        {
            //check if user is authorized to retireve bookings
            var authorizationResult = await _authorizationService.
                AuthorizeAsync(User, guestId, "GetBookingsByGuestPolicy");
            if (!authorizationResult.Succeeded)
            {
                return Unauthorized();
            }
            //call grpc service
            var reply = await _bookingClient.GetBookingsForGuestAsync(new Request { Id = guestId});
            //convert timestamp properties to datetime
            reply.Items.ToList().ForEach(x => ConvertBookingData(x));
            return Ok(reply.Items);
        }

        //retrieve bookings for accommodation indicated in pos
        [HttpGet("post/{postId}")]
        public async Task<IActionResult> GetBookingsForPost(long postId)
        {
            //check if user is authorized to retireve bookings
            var authorizationResult = await _authorizationService.
                AuthorizeAsync(User, postId, "GetBookingsByPostPolicy");
            if (!authorizationResult.Succeeded)
            {
                return Unauthorized();
            }
            //call grpc service
            var reply = await _bookingClient.GetBookingsForPostAsync(new Request { Id = postId });
            //convert timestamp properties to datetime
            reply.Items.ToList().ForEach(x => ConvertBookingData(x));
            return Ok(reply.Items);
        }

        //get booking by id
        //can be viewed only by user who created booking (guest) and by owner of 
        //accommodation on which booking is made
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooking(long id)
        {
            //check if user is authorized to retireve booking
            var authorizationResult = await _authorizationService.
                AuthorizeAsync(User, id, "GetByIdCancelBookingPolicy");
            if (!authorizationResult.Succeeded)
            {
                return Unauthorized();
            }
            //get booking
            var reply = await _bookingClient.GetBookingDetailsAsync(new Request { Id = id });
            return Ok(reply);
        }

        //create booking
        [HttpPost]
        public async Task<IActionResult> CreateBooking(CreateRequest request)
        {
            var userId = AuthorizationHelper.GetLoggedUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }
            //assign id of logged user as guest id 
            request.GuestId = userId;
            //call grpc service to create booking
            var reply = await _bookingClient
                .CreateBookingAsync(ConvertBookingData(request));
            //in case of errors, return bad request
            if (!reply.IsSuccess)
            {
                return BadRequest(reply);
            }
            //if successful, return created status code
            return StatusCode(201);
        }

        //change booking status to Accepted
        [HttpPatch("accept/{id}")]
        public async Task<IActionResult> AcceptBooking(long id)
        {
            //change booking status
            return await ProcessBooking(id, _bookingClient.AcceptBookingAsync,
                "AcceptRejectBookingPolicy");
        }

        //change booking status to Rejected
        [HttpPatch("reject/{id}")]
        public async Task<IActionResult> RejectBooking(long id)
        {
            //change booking status
            return await ProcessBooking(id, _bookingClient.RejectBookingAsync,
                "AcceptRejectBookingPolicy");
        }

        //change booking status to Rejected
        [HttpPatch("cancel/{id}")]
        public async Task<IActionResult> CancelBooking(long id)
        {
            //change booking status
            return await ProcessBooking(id, _bookingClient.CancelBookingAsync,
                "GetByIdCancelBookingPolicy");

        }

        //delete booking
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(long id)
        {
            //delete booking
            return await ProcessBooking(id, _bookingClient.DeleteBookingAsync,
                "DeleteBookingPolicy");
        }
        //convert time stamp to date time in booking
        private BookingInfoResponse ConvertBookingData(BookingInfoResponse booking)
        {
            booking.StartDate = (DateTime)DateTimeConversion.FromTimeStampToDateTime(booking.StartDateTimeStamp);
            booking.EndDate = (DateTime)DateTimeConversion.FromTimeStampToDateTime(booking.EndDateTimeStamp);
            return booking;
        }
        //convert date time to time stamp in booking
        private CreateRequest ConvertBookingData(CreateRequest booking)
        {
            booking.StartDateTimeStamp = DateTimeConversion.FromDateTimeToTimeStamp(booking.StartDate);
            booking.EndDateTimeStamp = DateTimeConversion.FromDateTimeToTimeStamp(booking.EndDate);
            return booking;
        }
        //method for booking status change and booking deletion
        private async Task<IActionResult> ProcessBooking(
            long id,
            Func<Request, Metadata, DateTime?, CancellationToken, AsyncUnaryCall<Response>> 
            grpcAction,
            string authPolicy)
        {
            //check if user is authorized to perform action
            var authorizationResult = await _authorizationService.
                AuthorizeAsync(User, id, authPolicy);
            if (!authorizationResult.Succeeded)
            {
                return Unauthorized();
            }
            //perform action
            var reply = await grpcAction(new Request { Id = id }, null, null, default);
            //return bad request in case of errors
            if (!reply.IsSuccess)
            {
                return BadRequest(reply);
            }
            return NoContent();
        }
    }
}
