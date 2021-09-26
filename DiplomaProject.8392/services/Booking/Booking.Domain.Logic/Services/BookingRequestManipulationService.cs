using AutoMapper;
using BaseClasses.Contracts;
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
        private readonly IMapper _mapper;

        public BookingRequestManipulationService(IRepository<BookingRequest> repository,
            IRepository<User> userRepository, IRepository<Accommodation> accommodationRepository,
            AbstractValidator<CreateBookingRequestDTO> validator,
            IMapper mapper)
        {
            _repository = repository;
            _userRepository = userRepository;
            _accommodationRepository = accommodationRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task AcceptBookingRequestAsync(long id)
        {
            await HandleRequestStatusAsync(Status.Accepted, id);
        }
        public async Task RejectBookingRequestAsync(long id)
        {
            await HandleRequestStatusAsync(Status.Rejected, id);
        }
       
        public async Task CancelBookingAsync(long id)
        {
            await HandleRequestStatusAsync(Status.Cancelled, id);
        }

        public async Task CreateBookingRequestAsync(CreateBookingRequestDTO requestDTO)
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
            var request = _mapper.Map<BookingRequest>(requestDTO);
            await _repository.CreateAsync(request);
        }
        public async Task DeleteBookingRequestAsync(long id)
        {
            var request = await FindRequestAsync(id);
            if(request.Status == Status.Accepted)
            {
                throw new DeleteBookingRequestException();
            }
            await _repository.DeleteAsync(request);
        }
        private async Task HandleRequestStatusAsync(Status status, long id)
        {
            var request = await FindRequestAsync(id);
            if(request.Status==status)
            {
                throw new BookingRequestStatusException(status);
            }
            request.SetStatus(status);
            await _repository.UpdateAsync(request);
        }
        private async Task<BookingRequest> FindRequestAsync(long id)
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
