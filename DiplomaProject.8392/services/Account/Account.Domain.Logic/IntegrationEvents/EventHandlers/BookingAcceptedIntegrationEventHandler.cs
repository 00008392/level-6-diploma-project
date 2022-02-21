using Account.Domain.Entities;
using Account.Domain.Logic.IntegrationEvents.EventHandlers.Core;
using Account.Domain.Logic.IntegrationEvents.Events;
using AutoMapper;
using DAL.Base.Contracts;
using EventBus.Contracts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.IntegrationEvents.EventHandlers
{
    //event handler that fires when booking request is accepted
    public class BookingAcceptedIntegrationEventHandler : BaseIntegrationEventHandler,
        IIntegrationEventHandler<BookingAcceptedIntegrationEvent>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<User> _userRepository;

        public BookingAcceptedIntegrationEventHandler(
            IRepository<Booking> repository,
            IMapper mapper,
            IRepository<User> userRepository) 
            :base(repository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        //when booking request is accepted and event is published,
        //account microservice consumes this event and creates new booking in database
        public async Task Handle(BookingAcceptedIntegrationEvent @event)
        {
            //check that both guest and accommodation owner exist in account database
            //and ensure that booking with same id does not exist
            if (_userRepository.DoesItemWithIdExist(@event.GuestId) &&
            _userRepository.DoesItemWithIdExist(@event.OwnerId) &&
            !_repository.DoesItemWithIdExist(@event.BookingId))
            {
                //if all correct, map to domain entity and save in the database
                var booking = _mapper.Map<Booking>(@event);
                await _repository.CreateAsync(booking);
            }
        }
    }
}
