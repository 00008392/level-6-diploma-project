using AutoMapper;
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
        private readonly IMapper _mapper;

        public BookingInfoServiceGrpc(IBookingInfoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
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
            var requestList = _mapper.Map<ICollection<BookingRequestInfoDTO>, 
                ICollection<BookingRequest>>(await action(id));
            var reply = new RequestInfoReply();
            reply.Requests.AddRange(requestList);
            return reply;
        }
    }
}
