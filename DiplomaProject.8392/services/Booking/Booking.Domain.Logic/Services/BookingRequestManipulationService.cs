using AutoMapper;
using BaseClasses.Contracts;
using Booking.Domain.Entities;
using Booking.Domain.Enums;
using Booking.Domain.Logic.Contracts;
using Booking.Domain.Logic.DTOs;
using Booking.Domain.Logic.Exceptions;
using Booking.Domain.Logic.Specification;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.Services
{
    public class BookingRequestManipulationService : IBookingService
    {
        private readonly IRepository<BookingRequest> _bookingRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Accommodation> _accommodationRepository;
        private readonly AbstractValidator<CreateBookingRequestDTO> _validator;
        private readonly IMapper _mapper;

        public BookingRequestManipulationService(IRepository<BookingRequest> repository,
            IRepository<User> userRepository, IRepository<Accommodation> accommodationRepository,
            AbstractValidator<CreateBookingRequestDTO> validator,
            IMapper mapper)
        {
            _bookingRepository = repository;
            _userRepository = userRepository;
            _accommodationRepository = accommodationRepository;
            _validator = validator;
            _mapper = mapper;
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
            if((await FindBookingForDatesAsync((DateTime)requestDTO.StartDate, 
                (DateTime)requestDTO.EndDate, requestDTO.AccommodationId)).Any())
            {
                throw new AccommodationBookedException(requestDTO.AccommodationId);
            }
            var request = _mapper.Map<BookingRequest>(requestDTO);
            await _bookingRepository.CreateAsync(request);
        }
        public async Task DeleteBookingRequestAsync(long id)
        {
            var request = await FindRequestAsync(id);
            if(request.Status == Status.Accepted)
            {
                throw new DeleteBookingRequestException();
            }
            await _bookingRepository.DeleteAsync(request);
        }
        public async Task ManipulateCoTravelerAsync(CoTravelerDTO coTravelerDTO, bool remove = false)
        {
            var coTraveler = await _userRepository.GetByIdAsync(coTravelerDTO.CoTravelerId);
            if(coTraveler==null)
            {
                throw new NotFoundException(coTravelerDTO.CoTravelerId, "Co traveler");
            }
            // co traveler can be added/removed only if booking request is not accepted yet
            var bookingRequest = (await _bookingRepository.GetFilteredAsync(
                x=> x.Id == coTravelerDTO.BookingId && x.Status == (int)Status.Pending)).SingleOrDefault();
            if(!_bookingRepository.DoesItemWithIdExist(coTravelerDTO.BookingId))
            {
                throw new NotFoundException(coTravelerDTO.CoTravelerId, "Booking request");
            }
            if(remove)
            {
                bookingRequest.CoTravelers.Remove(coTraveler);
            } else
            {
                bookingRequest.CoTravelers.Add(coTraveler);
            }
            await _bookingRepository.UpdateAsync(bookingRequest);

        }
        public async Task HandleRequestStatusAsync(long id, BookingRequestSpecification specification, Status status)
        {
            var request = await FindRequestAsync(id);
            bool isSatisfied = specification.IsSatisfiedBy(request);
            if (request.Status==status || !isSatisfied)
            {
                throw new BookingRequestStatusException(status);
            }
            request.SetStatus(status);
            await _bookingRepository.UpdateAsync(request);
        }
        public async Task<ICollection<BookingRequestInfoDTO>> GetBookings(BookingRequestSpecification specification)
        {
            var requestsList = (await _bookingRepository.GetFilteredAsync(specification.ToExpression())).ToList();
            var requestsDTOList = _mapper.Map<ICollection<BookingRequestInfoDTO>>(requestsList);
            return requestsDTOList;
        }
        private async Task<BookingRequest> FindRequestAsync(long id)
        {
            var request = await _bookingRepository.GetByIdAsync(id);
            if (request == null)
            {
                throw new NotFoundException(id, request.GetType().Name);
            }
            return request;
        }
        private async Task<ICollection<BookingRequest>> FindBookingForDatesAsync(DateTime startDate, DateTime endDate,
            long accommodationId)
        {
            var accommodations = await _bookingRepository.GetFilteredAsync(x => x.AccommodationId == accommodationId &&
              ((startDate >= x.StartDate && startDate <= x.EndDate) ||
              (endDate >= x.EndDate && endDate <= x.EndDate)) && x.Status == Status.Accepted);

            return accommodations;
        }


    }
}
