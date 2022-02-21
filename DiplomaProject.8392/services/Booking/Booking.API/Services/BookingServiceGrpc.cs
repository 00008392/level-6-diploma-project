using AutoMapper;
using Booking.Domain.Logic.Contracts;
using Booking.Domain.Logic.DTOs;
using EventBus.Contracts;
using FluentValidation;
using Grpc.Core;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Booking.Domain.Logic.Specifications;
using Booking.Domain.Enums;
using Booking.Domain.Logic.IntegrationEvents.Events;
using EventBus.Events;
using Grpc.Base.Helpers;
using Domain.Logic.Base.Specifications;

namespace Booking.API.Services
{
    //booking grpc service
    public class BookingServiceGrpc: BookingService.BookingServiceBase
    {
        //inject service from domain logic layer
        private readonly IBookingService _service;
        private readonly IMapper _mapper;

        public BookingServiceGrpc(
            IBookingService service,
            IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        //create booking
        public override async Task<Response> CreateBooking(CreateRequest request,
            ServerCallContext context)
        {
            //call helper method that handles create and update grpc actions
            return await GrpcServiceHelper.HandleCreateUpdateActionAsync<CreateBookingDTO,
                Response, CreateRequest>(_service.CreateBookingAsync, _mapper, request);
        }
        //delete booking
        public override async Task<Response> DeleteBooking(Request request,
          ServerCallContext context)
        {
            //call helper method that handles delete grpc action
            return await GrpcServiceHelper.HandleDeleteActionAsync<Response>
                (request.Id, _service.DeleteBookingAsync);
        }
        //update booking status to Accepted
        public override async Task<Response> AcceptBooking(Request request,
         ServerCallContext context)
        {
            return await HandleBookingStatusAsync<AcceptRejectBookingSpecification>
                (request, Domain.Enums.Status.Accepted);
        }
        //update booking status to Rejected
        public override async Task<Response> RejectBooking(Request request,
         ServerCallContext context)
        {
            return await HandleBookingStatusAsync<AcceptRejectBookingSpecification>
                 (request, Domain.Enums.Status.Rejected);
        }
        //update booking status to Cancelled
        public override async Task<Response> CancelBooking(Request request,
         ServerCallContext context)
        {
            return await HandleBookingStatusAsync<CancelBookingSpecification>
                (request, Domain.Enums.Status.Cancelled);
        }
        //retrieve bookings created by user (guest)
        public override async Task<BookingListResponse> GetBookingsForGuest(Request request,
            ServerCallContext context)
        {
            return await HandleBookingsRetrieval<BookingsByUserSpecification>
                (request);
        }
        //retrieve bookings for accommodation (indicated in post)
        public override async Task<BookingListResponse> GetBookingsForPost(Request request,
            ServerCallContext context)
        {
            return await HandleBookingsRetrieval<BookingsByPostSpecification>
                 (request);
        }
        //retrieve booking details by id
        public override async Task<BookingInfoResponse> GetBookingDetails(Request request,
            ServerCallContext context)
        {
            //call helper method that handles retrieval by id grpc action
            return await GrpcServiceHelper.HandleRetrievalByIdAsync<BookingInfoResponse,
                 BookingInfoDTO>(request.Id, _service.GetBookingDetailsAsync, _mapper);
        }
        //method that handles update of booking status
        private async Task<Response> HandleBookingStatusAsync<T>(
            Request request,
            Domain.Enums.Status status) where T: Specification<Domain.Entities.Booking>, new()
        {
            //create necessary specification
            var specification = new T();
            var bookingStatusDTO = new UpdateBookingStatusDTO(request.Id, specification, status);
            //call helper method that handles create and update grpc actions
            return await GrpcServiceHelper.HandleCreateUpdateActionAsync<UpdateBookingStatusDTO,
                 Response>(_service.HandleBookingStatusAsync, bookingStatusDTO);
        }
        //method that handles retrieval of bookings either by user or post
        private async Task<BookingListResponse> HandleBookingsRetrieval<T>
            (Request request) where T: Specification<Domain.Entities.Booking>
        {
            //create necessary specification
            var specification = (T)Activator.CreateInstance(typeof(T), request.Id);
            //get dtos from domain logic layer
            var bookings = await _service.GetBookingsAsync(specification);
            //map to response
            return GrpcServiceHelper.MapItems<BookingListResponse, BookingInfoDTO,
                BookingInfoResponse>(_mapper, bookings);
        }
    }
}
