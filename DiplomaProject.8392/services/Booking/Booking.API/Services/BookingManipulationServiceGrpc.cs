using Booking.Domain.Logic.Contracts;
using Booking.Domain.Logic.DTOs;
using ExceptionHandling;
using FluentValidation;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.API.Services
{
    public class BookingManipulationServiceGrpc: BookingRequestManipulation.BookingRequestManipulationBase
    {
        private readonly IBookingRequestManipulationService _service;

        public BookingManipulationServiceGrpc(IBookingRequestManipulationService service)
        {
            _service = service;
        }

        public override async Task<Response> CreateBookingRequest(CreateRequest request, 
            ServerCallContext context)
        {
            var createRequestDTO = new CreateBookingRequestDTO(request.GuestId ?? 0,
                    request.AccommodationId ?? 0, request.StartDate?.ToDateTime(),
                    request.EndDate?.ToDateTime());
           
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
            return await HandleRequestAsync(_service.AcceptBookingRequestAsync, request.Id);
        }
        public override async Task<Response> RejectBookingRequest(Request request,
         ServerCallContext context)
        {
            return await HandleRequestAsync(_service.RejectBookingRequestAsync, request.Id);
        }
        public override async Task<Response> CancelBooking(Request request,
         ServerCallContext context)
        {
            return await HandleRequestAsync(_service.CancelBookingAsync, request.Id);
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
    }
}
