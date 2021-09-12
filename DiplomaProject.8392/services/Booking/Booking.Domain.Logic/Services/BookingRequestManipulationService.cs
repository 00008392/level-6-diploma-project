﻿using BaseClasses.Contracts;
using Booking.Domain.Entities;
using Booking.Domain.Enums;
using Booking.Domain.Logic.Contracts;
using Booking.Domain.Logic.DTOs;
using Booking.Domain.Logic.Exceptions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.Services
{
    public class BookingRequestManipulationService : IBookingRequestManipulationService
    {
        private readonly IRepository<BookingRequest> _repository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Accommodation> _accommodationRepository;
        private readonly AbstractValidator<CreateBookingRequestDTO> _validator;

        public BookingRequestManipulationService(IRepository<BookingRequest> repository,
            IRepository<User> userRepository, IRepository<Accommodation> accommodationRepository,
            AbstractValidator<CreateBookingRequestDTO> validator)
        {
            _repository = repository;
            _userRepository = userRepository;
            _accommodationRepository = accommodationRepository;
            _validator = validator;
        }

        public async Task AcceptBookingRequest(long id)
        {
            await HandleRequestStatus(Status.Accepted, id);
        }
        public async Task RejectBookingRequest(long id)
        {
            await HandleRequestStatus(Status.Rejected, id);
        }
       
        public async Task CancelBooking(long id)
        {
            await HandleRequestStatus(Status.Cancelled, id);
        }

        public async Task CreateBookingRequest(CreateBookingRequestDTO requestDTO)
        {
            var validationResult = _validator.Validate(requestDTO);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var userExists = _userRepository.DoesItemWithIdExist(requestDTO.GuestId);
            if (!userExists)
            {
                throw new ForeignKeyViolationException("User");
            }
            var accommodationExists = _accommodationRepository.DoesItemWithIdExist(requestDTO.AccommodationId);
            if (!accommodationExists)
            {
                throw new ForeignKeyViolationException("Accommodation");
            }
            var request = new BookingRequest(requestDTO.GuestId, requestDTO.AccommodationId,
                (DateTime)requestDTO.StartDate, (DateTime)requestDTO.EndDate);
            await _repository.CreateAsync(request);
        }
        public async Task DeleteBookingRequest(long id)
        {
            var request = await FindRequest(id);
            if(request.Status == Status.Accepted)
            {
                throw new DeleteBookingRequestException();
            }
            await _repository.DeleteAsync(request);
        }
        private async Task HandleRequestStatus(Status status, long id)
        {
            var request = await FindRequest(id);
            if(request.Status==status)
            {
                throw new BookingRequestStatusException(status);
            }
            request.SetStatus(status);
            await _repository.UpdateAsync(request);
        }
        private async Task<BookingRequest> FindRequest(long id)
        {
            var request = await _repository.GetByIdAsync(id);
            if (request == null)
            {
                throw new NotFoundException(id, request.GetType().Name);
            }
            return request;
        }
    }
}
