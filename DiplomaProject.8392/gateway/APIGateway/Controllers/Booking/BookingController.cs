
using APIGateway.Helpers;
using APIGateway.QueryParameters;
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
    //[Route("api/[controller]")]
    //[ApiController]
    //public class BookingController : ControllerBase
    //{
    //    private readonly BookingService.BookingServiceClient _bookingClient;
    //    private delegate AsyncUnaryCall<Response> statusHandlingAction (Request request, Metadata headers = null, DateTime? deadline = null,
    //        CancellationToken cancellationToken = default);
    //    public BookingController(BookingService.BookingServiceClient bookingClient)
    //    {
    //        _bookingClient = bookingClient;
    //    }
    //    // GET: api/<BookingController>
    //    [HttpGet]
    //    public async Task<IActionResult> Get([FromQuery] UserAccommodationQueryParameter query)
    //    {
    //        if(query==null)
    //        {
    //            return BadRequest();
    //        }
    //       var request = new GetBookingsRequest();
    //       if(query.User!=null)
    //        {
    //            request.Id = (long)query.User;
    //            request.ForAction = GetBookingsRequest.Types.ForAction.ForUser;
    //        }
    //        if (query.Accommodation != null)
    //        {
    //            request.Id = (long)query.Accommodation;
    //            request.ForAction = GetBookingsRequest.Types.ForAction.ForAccommodation;

    //        }
    //        var reply = await _bookingClient.GetBookingsAsync(request);
    //        reply.Bookings.ToList().ForEach(x => ConvertBookingData(x));
    //        return Ok(reply.Bookings);
    //    }

    //    // GET api/<BookingController>/5
    //    [HttpGet("{id}")]
    //    public async Task<IActionResult> Get(long id)
    //    {
    //        var request = new Request
    //        {
    //            Id = id
    //        };
    //        var reply = await _bookingClient.GetBookingDetailsAsync(request);
    //        if(reply.NoBooking)
    //        {
    //            return NotFound("Booking not found");
    //        }
    //        return Ok(reply);
    //    }

    //    // POST api/<BookingController>
    //    [Authorize]
    //    [HttpPost]
    //    public async Task<IActionResult> Post(CreateRequest request)
    //    {
    //        if(GetLoggedUserId()!=request.GuestId)
    //        {
    //            return Unauthorized();
    //        }
    //        var reply = await _bookingClient.CreateBookingRequestAsync(ConvertBookingData(request));
    //        if(!reply.IsSuccess)
    //        {
    //            return BadRequest(reply);
    //        }
    //        return StatusCode(201);
    //    }
    //    [HttpPost("co-traveler")]
    //    public async Task<IActionResult> Post(CoTravelerRequest request)
    //    {
    //        var reply = await _bookingClient.AddCoTravelerAsync(request);
    //        if (!reply.IsSuccess)
    //        {
    //            return BadRequest(reply);
    //        }
    //        return StatusCode(201);
    //    }
    //    // PUT api/<BookingController>/5
    //    [Authorize]
    //    [HttpPatch("accept/{id}")]
    //    public async Task<IActionResult> AcceptBooking(long id)
    //    {
    //        var booking = await _bookingClient.GetBookingDetailsAsync(new Request { Id = id});
    //        if(booking.Accommodation?.OwnerId!=GetLoggedUserId())
    //        {
    //            return Unauthorized();
    //        }
    //        return await ProcessBookingStatus(id, _bookingClient.AcceptBookingRequestAsync);
    //    }
    //    [Authorize]
    //    [HttpPatch("reject/{id}")]
    //    public async Task<IActionResult> RejectBooking(long id)
    //    {
    //        var booking = await _bookingClient.GetBookingDetailsAsync(new Request { Id = id });
    //        if (booking.Accommodation?.OwnerId != GetLoggedUserId())
    //        {
    //            return Unauthorized();
    //        }
    //        return await ProcessBookingStatus(id, _bookingClient.RejectBookingRequestAsync);
    //    }
    //    [Authorize]
    //    [HttpPatch("cancel/{id}")]
    //    public async Task<IActionResult> CancelBooking(long id)
    //    {
    //        var booking = await _bookingClient.GetBookingDetailsAsync(new Request { Id = id });
    //        if (booking.Guest?.Id != GetLoggedUserId())
    //        {
    //            return Unauthorized();
    //        }
    //        return await ProcessBookingStatus(id, _bookingClient.CancelBookingAsync);
    //    }
    //    // DELETE api/<BookingController>/5
    //    [Authorize]
    //    [HttpDelete("{id}")]
    //    public async Task<IActionResult> Delete(long id)
    //    {
    //        var request = new Request
    //        {
    //            Id = id
    //        };
    //        var booking = await _bookingClient.GetBookingDetailsAsync(request);
    //        if (booking.Guest?.Id != GetLoggedUserId())
    //        {
    //            return Unauthorized();
    //        }
    //        var reply = await _bookingClient.DeleteBookingRequestAsync(request);
    //        if(!reply.IsSuccess)
    //        {
    //            return BadRequest(reply);
    //        }
    //        return NoContent();
    //    }
    //    [HttpDelete("co-traveler")]
    //    public async Task<IActionResult> Delete(CoTravelerRequest request)
    //    {
    //        var reply = await _bookingClient.RemoveCoTravelerAsync(request);
    //        if (!reply.IsSuccess)
    //        {
    //            return NotFound(reply);
    //        }
    //        return NoContent();
    //    }
    //    private BookingDetailsReply ConvertBookingData(BookingDetailsReply booking)
    //    {
    //        booking.StartDate = (DateTime)DateTimeConversion.FromTimeStampToDateTime(booking.StartDateTimeStamp);
    //        booking.EndDate = (DateTime)DateTimeConversion.FromTimeStampToDateTime(booking.EndDateTimeStamp);
    //        return booking;
    //    }
    //    private CreateRequest ConvertBookingData(CreateRequest booking)
    //    {
    //        booking.StartDateTimeStamp = DateTimeConversion.FromDateTimeToTimeStamp(booking.StartDate);
    //        booking.EndDateTimeStamp = DateTimeConversion.FromDateTimeToTimeStamp(booking.EndDate);
    //        return booking;
    //    }
    //    private async Task<IActionResult> ProcessBookingStatus(long id, statusHandlingAction action)
    //    {
    //        var request = new Request
    //        {
    //            Id = id
    //        };
    //        var reply = await action(request);
    //        if (!reply.IsSuccess)
    //        {
    //            return BadRequest(reply);
    //        }
    //        return NoContent();
    //    }
    //    private int GetLoggedUserId()
    //    {
    //        return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
    //    }
    //}
}
