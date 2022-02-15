using AutoMapper;
using BaseClasses.Contracts;
using BaseClasses.Exceptions;
using EventBus.Contracts;
using FluentValidation;
using Domain.Helpers;
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

namespace PostFeedback.Domain.Logic.IntegrationEvents.EventHandlers
{
    //event handler that fires when booking request is accepted
    public class BookingAcceptedIntegrationEventHandler : BookingBaseIntegrationEventHandler,
        IIntegrationEventHandler<BookingAcceptedIntegrationEvent>
    {
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<User> _userRepository;
        private readonly AbstractValidator<BookingAcceptedIntegrationEvent> _validator;
        private readonly IMapper _mapper;

        public BookingAcceptedIntegrationEventHandler(
            IRepository<Post> postRepository,
            IRepository<User> userRepository,
            IRepository<Booking> bookingRepository,
            AbstractValidator<BookingAcceptedIntegrationEvent> validator,
            IMapper mapper):base(bookingRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task Handle(BookingAcceptedIntegrationEvent @event)
        {
            //check if post with id indicated in event exists
            ServiceHelper.CheckIfRelatedEntityExists(@event.PostId, _postRepository);
            //check if guest with id indicated in event exists
            ServiceHelper.CheckIfRelatedEntityExists(@event.GuestId, _userRepository);
            //validate booking
            ServiceHelper.ValidateItem(_validator, @event);
            //if all correct, map to domain entity and insert into the database
            var booking = _mapper.Map<Booking>(@event);
            await _bookingRepository.CreateAsync(booking);
        }
    }
}
