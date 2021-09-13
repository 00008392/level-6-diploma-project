using Booking.Domain.Logic.Contracts;
using Booking.Domain.Logic.DTOs;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.API.Services
{
    public class BookingInfoServiceGrpc: BookingRequestInfo.BookingRequestInfoBase
    {
        private readonly IBookingInfoService _service;

        public BookingInfoServiceGrpc(IBookingInfoService service)
        {
            _service = service;
        }

        public override async Task<RequestInfoReply> GetRequestsForUser(Request request,
            ServerCallContext context)
        {
            return await HandleInfoAsync(_service.GetBookingRequestsForUserAsync, request.Id);
        }
        public override async Task<RequestInfoReply> GetRequestsForAccommodation(Request request,
            ServerCallContext context)
        {
            return await HandleInfoAsync(_service.GetBookingRequestsForAccommodationAsync, request.Id);
        }

        private async Task<RequestInfoReply> HandleInfoAsync(Func<long, Task<ICollection<BookingRequestInfoDTO>>> action, 
            long id)
        {
            var requestList = (await action(id)).Select(x => new BookingRequest
            {
                Guest = x.Guest == null ? null : new User
                {
                    Id = x.Guest.Id,
                    FirstName = x.Guest.FirstName,
                    LastName = x.Guest.LastName,
                    Email = x.Guest.Email,
                    PhoneNumber = x.Guest.PhoneNumber,
                    Address = x.Guest.Address,
                    DateOfBirth = x.Guest.DateOfBirth == null ? null : Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)x.Guest.DateOfBirth, DateTimeKind.Utc))
                },
                Accommodation = x.Accommodation == null ? null : new Accommodation
                {
                    Id = x.Accommodation.Id,
                    Title = x.Accommodation.Title,
                    OwnerId = x.Accommodation.OwnerId,
                    Address = x.Accommodation.Address,
                    ContactNumber = x.Accommodation.ContactNumber,
                    RoomsNo = x.Accommodation.RoomsNo,
                    BathroomsNo = x.Accommodation.BathroomsNo,
                    BedsNo = x.Accommodation.BedsNo,
                    MaxGuestsNo = x.Accommodation.MaxGuestsNo,
                    SquareMeters = x.Accommodation.SquareMeters,
                    Price = (double)x.Accommodation.Price,
                    IsWholeApartment = x.Accommodation.IsWholeApartment,
                    MovingInTime = x.Accommodation.MovingInTime,
                    MovingOutTime = x.Accommodation.MovingOutTime
                },
                StartDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.SpecifyKind(x.StartDate, DateTimeKind.Utc)),
                EndDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.SpecifyKind(x.EndDate, DateTimeKind.Utc)),
                Status = (int)x.Status
            }).ToList();
            var reply = new RequestInfoReply();
            reply.Requests.AddRange(requestList);
            return reply;
        }
    }
}
