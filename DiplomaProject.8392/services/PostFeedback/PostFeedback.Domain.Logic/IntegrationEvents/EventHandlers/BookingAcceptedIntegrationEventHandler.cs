using AutoMapper;
using EventBus.Contracts;
using FluentValidation;
using PostFeedback.Domain.Entities;
using PostFeedback.Domain.Logic.Contracts;
using PostFeedback.Domain.Logic.DTOs;
using PostFeedback.Domain.Logic.IntegrationEvents.EventHandlers.Core;
using PostFeedback.Domain.Logic.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Base.Contracts;

namespace PostFeedback.Domain.Logic.IntegrationEvents.EventHandlers
{
    //event handler that fires when booking request is accepted
    public class BookingAcceptedIntegrationEventHandler : BookingBaseIntegrationEventHandler,
        IIntegrationEventHandler<BookingAcceptedIntegrationEvent>
    {
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public BookingAcceptedIntegrationEventHandler(
            IRepository<Post> postRepository,
            IRepository<User> userRepository,
            IRepository<Booking> bookingRepository,
            IMapper mapper)
            :base(bookingRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task Handle(BookingAcceptedIntegrationEvent @event)
        {
            //if post and guest exist in the database
            //and booking with given id does not exist, map to domain entity and insert into the database
            if(_postRepository.DoesItemWithIdExist(@event.PostId)&&
                _userRepository.DoesItemWithIdExist(@event.GuestId)&&
                !_bookingRepository.DoesItemWithIdExist(@event.BookingId))
            {
                var booking = _mapper.Map<Booking>(@event);
                await _bookingRepository.CreateAsync(booking);
            }
        }
    }
}
