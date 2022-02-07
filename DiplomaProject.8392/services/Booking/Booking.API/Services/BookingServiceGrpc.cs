using AutoMapper;
using Booking.Domain.Logic.Contracts;
using Booking.Domain.Logic.DTOs;
using EventBus.Contracts;
using ExceptionHandlingAPI;
using FluentValidation;
using Grpc.Core;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Booking.Domain.Logic.Enums;
using Booking.Domain.Logic.Specification;
using Booking.Domain.Enums;
using Booking.Domain.Logic.IntegrationEvents.Events;
using EventBus.Events;

namespace Booking.API.Services
{
    public class BookingServiceGrpc: BookingService.BookingServiceBase
    {
        private readonly IBookingService _service;
        private readonly IMapper _mapper;
        private readonly IEventBus _eventBus;

        public BookingServiceGrpc(IBookingService service,
            IMapper mapper, IEventBus eventBus)
        {
            _service = service;
            _mapper = mapper;
            _eventBus = eventBus;
        }

        public override async Task<Response> CreateBookingRequest(CreateRequest request,
            ServerCallContext context)
        {
            var createRequestDTO = _mapper.Map<CreateBookingRequestDTO>(request);

            return await HandleRequestAsync(_service.CreateBookingRequestAsync, createRequestDTO);
        }
        public override async Task<Response> DeleteBookingRequest(Request request,
          ServerCallContext context)
        {
            return await HandleRequestAsync(_service.DeleteBookingRequestAsync, request.Id);
        }
        public override async Task<Response> AcceptBookingRequest(Request request,
         ServerCallContext context)
        {
            var reply = await HandleBookingStatusAsync<AcceptRejectBookingRequestSpecification>
                 (request, Domain.Enums.Status.Accepted);
            await PublishStatusChangeEvent<BookingAcceptedIntegrationEvent>(reply, request.Id);
            return reply;
        }
        public override async Task<Response> RejectBookingRequest(Request request,
         ServerCallContext context)
        {
            return await HandleBookingStatusAsync<AcceptRejectBookingRequestSpecification>
                (request, Domain.Enums.Status.Rejected);
        }
        public override async Task<Response> CancelBooking(Request request,
         ServerCallContext context)
        {
            var reply = await HandleBookingStatusAsync<CancelBookingRequestSpecification>
                (request, Domain.Enums.Status.Cancelled);
            await PublishStatusChangeEvent<BookingCancelledIntegrationEvent>(reply, request.Id);
            return reply;
        }
        public override async Task<Response> AddCoTraveler(CoTravelerRequest request,
            ServerCallContext context)
        {
            return await HandleCoTravelerAsync(request, CoTravelerAction.Add);
        }
        public override async Task<Response> RemoveCoTraveler(CoTravelerRequest request,
           ServerCallContext context)
        {
            return await HandleCoTravelerAsync(request, CoTravelerAction.Remove);
        }
        public override async Task<BookingsReply> GetBookings(GetBookingsRequest request,
            ServerCallContext context)
        {
            var reply = new BookingsReply();
            BookingRequestSpecification specification = null;
            if (request.ForAction == GetBookingsRequest.Types.ForAction.ForUser)
            {
                specification = new BookingsByUserSpecification(request.Id);
            }
            else if (request.ForAction == GetBookingsRequest.Types.ForAction.ForAccommodation)
            {
                specification = new BookingsByAccommodationSpecification(request.Id);
            } 
            if(specification != null)
            {
                var bookings =_mapper.Map<ICollection<BookingDetailsReply>>(await _service.GetBookingsAsync(specification));
                reply.Bookings.AddRange(bookings);
            }
            return reply;
            
        }
        public override async Task<BookingDetailsReply> GetBookingDetails(Request request,
            ServerCallContext context)
        {
            var bookingDetails = await _service.GetBookingDetailsAsync(request.Id);
            if(bookingDetails!=null)
            {
                return _mapper.Map<BookingDetailsReply>(bookingDetails);
            }
            return new BookingDetailsReply
            {
                NoBooking = true
            };
        }
        private async Task PublishStatusChangeEvent<T>(Response reply, long bookingId) where T: IntegrationEvent
        {
            if (reply.IsSuccess)
            {
                var bookingStatusEvent = _mapper.Map<T>(
                    await _service.GetBookingDetailsAsync(bookingId)
                    );
                _eventBus.Publish(bookingStatusEvent);
            }
        }
        private async Task<Response> HandleCoTravelerAsync(CoTravelerRequest request, CoTravelerAction action)
        {
            var coTravelerDTO = new CoTravelerDTO(request.BookingId, request.CoTravelerId, action);
            return await HandleRequestAsync(_service.HandleCoTravelerAsync, coTravelerDTO);
        }
        private async Task<Response> HandleRequestAsync<T>(Func<T, Task> action, T parameter)
        {
            var response = new Response();
            try
            {
                await action(parameter);
                response.IsSuccess = true;
            }
            catch (ValidationException ex)
            {
                response.HandleValidationException(ex);
            }
            catch (Exception ex)
            {
                response.HandleException(ex);
            }
            return response;
        }
        private async Task<Response> HandleBookingStatusAsync<T>(Request request,
            Domain.Enums.Status status) where T: BookingRequestSpecification, new()
        {
            var specification = new T();
            var bookingStatusDTO = new BookingStatusDTO(request.Id, specification, status);
            return await HandleRequestAsync(_service.HandleRequestStatusAsync, bookingStatusDTO);
        }
    }
}
