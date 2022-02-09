using Account.Domain.Entities;
using Account.Domain.Logic.IntegrationEvents.EventHandlers.Core;
using Account.Domain.Logic.IntegrationEvents.Events;
using AutoMapper;
using BaseClasses.Contracts;
using BaseClasses.Exceptions;
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
        private readonly AbstractValidator<BookingAcceptedIntegrationEvent> _validator;
        private readonly IMapper _mapper;
        private readonly IRepository<User> _userRepository;

        public BookingAcceptedIntegrationEventHandler(
            AbstractValidator<BookingAcceptedIntegrationEvent> validator,
            IRepository<Booking> repository,
            IMapper mapper,
            IRepository<User> userRepository) 
            :base(repository)
        {
            _validator = validator;
            _mapper = mapper;
            _userRepository = userRepository;
        }
        //when booking request is accepted and event is published,
        //account microservice consumes this event and creates new booking in database
        public async Task Handle(BookingAcceptedIntegrationEvent @event)
        {
            //validate booking before creation
            var result = _validator.Validate(@event);
            if (result.IsValid)
            {
                //check that both guest and accommodation owner exist in account database
                if (!_userRepository.DoesItemWithIdExist(@event.GuestId) ||
               !_userRepository.DoesItemWithIdExist(@event.OwnerId))
                {
                    throw new ForeignKeyViolationException("User");
                }
                //ensure that booking with same id does not exist (because auto increment is off for PK)
                if(_repository.DoesItemWithIdExist(@event.BookingId))
                {
                    throw new UniqueConstraintViolationException("Booking", $"id={@event.BookingId}");
                }
                //if all correct, map to domain entity and save in the database
                var booking = _mapper.Map<Booking>(@event);
                await _repository.CreateAsync(booking);
            }
            else
            {
                //in case if validation errors occur
                throw new ValidationException(result.Errors);
            }
           
        }
    }
}
