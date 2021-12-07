﻿using APIGateway.Controllers.Booking.QueryParameters;
using APIGateway.Helpers;
using Booking.API;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIGateway.Controllers.Booking
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly BookingService.BookingServiceClient _bookingClient;
        private delegate AsyncUnaryCall<Response> statusHandlingAction (Request request, Metadata headers = null, DateTime? deadline = null,
            CancellationToken cancellationToken = default);
        public BookingController(BookingService.BookingServiceClient bookingClient)
        {
            _bookingClient = bookingClient;
        }
        // GET: api/<BookingController>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetBookingsQueryParameter query)
        {
            if(query==null)
            {
                return BadRequest();
            }
           var request = new GetBookingsRequest();
           if(query.User!=null)
            {
                request.Id = (long)query.User;
                request.ForAction = GetBookingsRequest.Types.ForAction.ForUser;
            }
            if (query.Accommodation != null)
            {
                request.Id = (long)query.Accommodation;
                request.ForAction = GetBookingsRequest.Types.ForAction.ForAccommodation;

            }
            var reply = await _bookingClient.GetBookingsAsync(request);
            reply.Bookings.ToList().ForEach(x => ConvertBookingData(x));
            return Ok(reply);
        }

        // GET api/<BookingController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var request = new Request
            {
                Id = id
            };
            var reply = await _bookingClient.GetBookingDetailsAsync(request);
            if(reply.NoBooking)
            {
                return NotFound("Booking not found");
            }
            return Ok(reply);
        }

        // POST api/<BookingController>
        [HttpPost]
        public async Task<IActionResult> Post(CreateRequest request)
        {
            var reply = await _bookingClient.CreateBookingRequestAsync(ConvertBookingData(request));
            if(!reply.IsSuccess)
            {
                return BadRequest(reply);
            }
            return StatusCode(201);
        }
        [HttpPost("co-traveler")]
        public async Task<IActionResult> Post(CoTravelerRequest request)
        {
            var reply = await _bookingClient.AddCoTravelerAsync(request);
            if (!reply.IsSuccess)
            {
                return BadRequest(reply);
            }
            return StatusCode(201);
        }
        // PUT api/<BookingController>/5
        [HttpPatch("accept/{id}")]
        public async Task<IActionResult> AcceptBooking(Request request)
        {
            return await ProcessBookingStatus(request, _bookingClient.AcceptBookingRequestAsync);
        }
        [HttpPatch("reject/{id}")]
        public async Task<IActionResult> RejectBooking(Request request)
        {
            return await ProcessBookingStatus(request, _bookingClient.RejectBookingRequestAsync);
        }
        [HttpPatch("cancel/{id}")]
        public async Task<IActionResult> CancelBooking(Request request)
        {
            return await ProcessBookingStatus(request, _bookingClient.CancelBookingAsync);
        }
        // DELETE api/<BookingController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var request = new Request
            {
                Id = id
            };
            var reply = await _bookingClient.DeleteBookingRequestAsync(request);
            if(!reply.IsSuccess)
            {
                return NotFound(reply);
            }
            return NoContent();
        }
        [HttpDelete("co-traveler")]
        public async Task<IActionResult> Delete(CoTravelerRequest request)
        {
            var reply = await _bookingClient.RemoveCoTravelerAsync(request);
            if (!reply.IsSuccess)
            {
                return NotFound(reply);
            }
            return NoContent();
        }
        private BookingDetailsReply ConvertBookingData(BookingDetailsReply booking)
        {
            booking.StartDate = (DateTime)DateTimeConversion.FromTimeStampToDateTime(booking.StartDateTimeStamp);
            booking.EndDate = (DateTime)DateTimeConversion.FromTimeStampToDateTime(booking.EndDateTimeStamp);
            booking.CoTravelers.ToList().ForEach(x => ConvertUserData(x));
            return booking;
        }
        private User ConvertUserData(User user)
        {
            user.DateOfBirth = (DateTime)DateTimeConversion.FromTimeStampToDateTime(user.DateOfBirthTimeStamp);
            return user;
        }
        private CreateRequest ConvertBookingData(CreateRequest booking)
        {
            booking.StartDateTimeStamp = DateTimeConversion.FromDateTimeToTimeStamp(booking.StartDate);
            booking.EndDateTimeStamp = DateTimeConversion.FromDateTimeToTimeStamp(booking.EndDate);
            return booking;
        }
        private async Task<IActionResult> ProcessBookingStatus(Request request, statusHandlingAction action)
        {
            var reply = await action(request);
            if (!reply.IsSuccess)
            {
                return BadRequest(reply);
            }
            return NoContent();
        } 
    }
}
