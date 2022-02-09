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
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.Services
{
    public class BookingService : IBookingService
    {
        private readonly IRepositoryWithIncludes<BookingRequest> _bookingRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Accommodation> _accommodationRepository;
        private readonly AbstractValidator<CreateBookingRequestDTO> _validator;
        private readonly IMapper _mapper;

        public BookingService(IRepositoryWithIncludes<BookingRequest> repository,
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
            var accommodation = await _accommodationRepository.GetByIdAsync(requestDTO.AccommodationId);
            if (accommodation==null)
            {
                throw new ForeignKeyViolationException("Accommodation");
            }
            if(requestDTO.GuestId==accommodation.OwnerId)
            {
                throw new Exception("Accommodation cannot be booked by its owner");
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
            await _bookingRepository.DeleteAsync(id);
        }
        public async Task HandleCoTravelerAsync(CoTravelerDTO coTravelerDTO)
        {
            var coTraveler = await _userRepository.GetByIdAsync(coTravelerDTO.CoTravelerId);
            if (coTraveler == null)
            {
                throw new NotFoundException(coTravelerDTO.CoTravelerId, "Co traveler");
            }
            // co traveler can be added/removed only if booking request is not accepted yet
            var bookingRequest = (await _bookingRepository.GetFilteredAsync(
                x => x.Id == coTravelerDTO.BookingId && x.Status == (int)Status.Pending, 
                relatedEntitiesIncluded: true))
                .SingleOrDefault();
            if (bookingRequest==null)
            {
                throw new NotFoundException(coTravelerDTO.CoTravelerId, "Booking request");
            }
            if(coTravelerDTO.Action==Enums.CoTravelerAction.Remove)
            {
                var coTravelerToDelete = bookingRequest.CoTravelers.Where(x => x.Id == coTraveler.Id).SingleOrDefault();
                    if (coTravelerToDelete!=null)
                    {
                        bookingRequest.RemoveCotraveler(coTravelerToDelete);
                    }
            }
            else
            {
                if (!bookingRequest.CoTravelers.Where(x => x.Id == coTraveler.Id).Any())
                {
                    bookingRequest.AddCotraveler(coTraveler);
                }
            }
            await _bookingRepository.UpdateAsync(bookingRequest);
        }
        public async Task HandleRequestStatusAsync(BookingStatusDTO bookingStatusDTO)
        {
            var request = await FindRequestAsync(bookingStatusDTO.BookingId);
            bool isSatisfied = bookingStatusDTO.BookingSpecification.IsSatisfiedBy(request);
            if (request.Status== bookingStatusDTO.BookingStatus || !isSatisfied)
            {
                throw new BookingRequestStatusException(bookingStatusDTO.BookingStatus);
            }
            request.SetStatus(bookingStatusDTO.BookingStatus);
            await _bookingRepository.UpdateAsync(request);
        }
        public async Task<ICollection<BookingInfoDTO>> GetBookingsAsync(BookingRequestSpecification specification)
        {
            var requestsList = (await _bookingRepository.GetFilteredAsync(specification.ToExpression(), relatedEntitiesIncluded: true)).ToList();
            var requestsDTOList = _mapper.Map<ICollection<BookingInfoDTO>>(requestsList);
            return requestsDTOList;
        }
        public async Task<BookingInfoDTO> GetBookingDetailsAsync(long id)
        {
            var request = await _bookingRepository.GetByIdAsync(id, relatedEntitiesIncluded: true);
            if (request!=null)
            {
                return _mapper.Map<BookingInfoDTO>(request);
            }
            return null;
        }
        private async Task<BookingRequest> FindRequestAsync(long id)
        {
            var request = await _bookingRepository.GetByIdAsync(id);
            if (request == null)
            {
                throw new NotFoundException("Booking request");
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
