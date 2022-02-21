using AutoMapper;
using Booking.Domain.Entities;
using Booking.Domain.Enums;
using Booking.Domain.Logic.Contracts;
using Booking.Domain.Logic.DTOs;
using Booking.Domain.Logic.Exceptions;
using Booking.Domain.Logic.IntegrationEvents.Events;
using Booking.Domain.Logic.Specifications;
using DAL.Base.Contracts;
using Domain.Logic.Base.Exceptions;
using Domain.Logic.Base.Helpers;
using Domain.Logic.Base.Specifications;
using EventBus.Contracts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.Services
{
    //service for booking manipulation
    public class BookingService : IBookingService
    {
        private readonly IRepository<Entities.Booking> _bookingRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Post> _postRepository;
        private readonly AbstractValidator<CreateBookingDTO> _validator;
        private readonly IMapper _mapper;
        private readonly IEventBus _eventBus;

        public BookingService(
            IRepository<Entities.Booking> repository,
            IRepository<User> userRepository,
            IRepository<Post> postRepository,
            AbstractValidator<CreateBookingDTO> validator,
            IMapper mapper,
            IEventBus eventBus)
        {
            _bookingRepository = repository;
            _userRepository = userRepository;
            _postRepository = postRepository;
            _validator = validator;
            _mapper = mapper;
            _eventBus = eventBus;
        }
        //create booking request on accommodation
        public async Task CreateBookingAsync(CreateBookingDTO requestDTO)
        {
            //validate dto
            ServiceHelper.ValidateItem(_validator, requestDTO);
            //ensure that related entities exist in the DB
            //check that guest exists
            ServiceHelper.CheckIfRelatedEntityExists(requestDTO.GuestId, _userRepository);
            //check that post exists
            ServiceHelper.CheckIfRelatedEntityExists(requestDTO.PostId, _postRepository);
            //accommodation (post) owner cannot book his own accommodation
            var post = await _postRepository.GetByIdAsync(requestDTO.PostId);
            //ensure that guest is not the same as owner
            if (requestDTO.GuestId==post.OwnerId)
            {
                throw new AccommodationBookedByOwnerException();
            }
            //ensure that number of guests indicated in booking is not greater than
            //maximum number of guests that accommdoation can have
            if(requestDTO.GuestNo>post.MaxGuestsNo)
            {
                throw new InvalidGuestNumberException();
            }
            //ensure that accommodation is not booked for specified period of time
            //find booking for the same accommodation and the same period of time
            //if such booking exists, accommodation cannot be booked
            var booking = (await _bookingRepository.GetFilteredAsync(new BookingsByPostAndDatesSpecification(
                (DateTime)requestDTO.StartDate, (DateTime)requestDTO.EndDate, requestDTO.PostId)
                .ToExpression())).FirstOrDefault();
            if (booking !=null)
            {
                throw new AccommodationBookedException(requestDTO.PostId);
            }
            //if all correct, map dto to domain entity
            var request = _mapper.Map<Entities.Booking>(requestDTO);
            //create booking
            await _bookingRepository.CreateAsync(request);
        }
        //delete booking
        //can be deleted by the user who requested it until not accepted or if rejected/cancelled
        public async Task DeleteBookingAsync(long id)
        {
            //find booking in the DB and throw exception if does not exist
            var request = await FindRequestAsync(id);
            //booking cannot be deleted if it is accepted
            if(request.Status == Status.Accepted)
            {
                throw new DeleteBookingException();
            }
            //if all correct, delete booking
            await _bookingRepository.DeleteAsync(id);
        }
        //update booking status
        //can be accepted/rejected by accommodation owner, can be cancelled by both guest and owner
        public async Task HandleBookingStatusAsync(UpdateBookingStatusDTO bookingStatusDTO)
        {
            //find booking in the DB and throw exception if does not exist
            var booking = await FindRequestAsync(bookingStatusDTO.BookingId, x=>x.Post);
            //ensure that booking status can be changed to indicated status through specification
            bool isSatisfied = bookingStatusDTO.BookingSpecification.IsSatisfiedBy(booking);
            //if user sets the same status or if condition in specification is not met, throw exception
            if (booking.Status==bookingStatusDTO.Status || !isSatisfied)
            {
                throw new UpdateBookingStatusException(bookingStatusDTO.Status);
            }
            //if all correct, set status
            booking.SetStatus(bookingStatusDTO.Status);
            //update booking
            await _bookingRepository.UpdateAsync(booking);
            //booking microservice publishes integration event in case if 
            //booking is accepted by accommodation owner or if accepted booking is cancelled
            //by either guest or owner
            PublishIntegrationEvent(booking);
        }
        //get bookings either by guest or by post
        //guest can view bookings created by him/her and owner can view bookings made on his/her accommodations
        public async Task<ICollection<BookingInfoDTO>> GetBookingsAsync(Specification<Entities.Booking> specification)
        {
            //filter bookings by specification
            var bookingList = (await _bookingRepository.
                GetFilteredAsync(specification.ToExpression(), x=>x.Post)).ToList();
            //map entities to dtos
            var bookingDTOList = _mapper.Map<ICollection<BookingInfoDTO>>(bookingList);
            return bookingDTOList;
        }
        //get booking details by id
        public async Task<BookingInfoDTO> GetBookingDetailsAsync(long id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id, x=>x.Post);
            if (booking != null)
            {
                //map domain entity to dto
                return _mapper.Map<BookingInfoDTO>(booking);
            }
            return null;
        }
        //method that finds booking by id in the DB and throws exception if it does not exist
        private async Task<Entities.Booking> FindRequestAsync(
            long id,
            params Expression<Func<Entities.Booking, object>>[] includes)
        {
            var booking = await _bookingRepository.GetByIdAsync(id, includes);
            if (booking == null)
            {
                throw new NotFoundException(id,nameof(Entities.Booking));
            }
            return booking;
        }
        //method that publishes integration event through event bus when booking is accepted/cancelled
        private void PublishIntegrationEvent(Entities.Booking booking)
        {
            if (booking.Status == Status.Accepted)
            {
                var integrationEvent = _mapper.Map<BookingAcceptedIntegrationEvent>(booking);
                _eventBus.Publish(integrationEvent);
            }
            else if (booking.Status == Status.Cancelled)
            {
                _eventBus.Publish(new BookingCancelledIntegrationEvent(booking.Id));
            }
        }
    }
}
