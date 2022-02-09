using Account.Domain.Entities;
using Account.Domain.Logic.IntegrationEvents.EventHandlers.Core;
using Account.Domain.Logic.IntegrationEvents.Events;
using BaseClasses.Contracts;
using BaseClasses.Exceptions;
using EventBus.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.IntegrationEvents.EventHandlers
{
    //event handler that fires when booking is cancelled
    public class BookingCancelledIntegrationEventHandler : BaseIntegrationEventHandler,
        IIntegrationEventHandler<BookingCancelledIntegrationEvent>
    {
        public BookingCancelledIntegrationEventHandler(IRepository<Booking> repository)
            :base(repository)
        {
        }
        //when booking is cancelled and event is published,
        //account microservice consumes this event and removes booking from database
        public async Task Handle(BookingCancelledIntegrationEvent @event)
        {
            //ensure that booking exists
            if (!_repository.DoesItemWithIdExist(@event.BookingId))
            {
                throw new NotFoundException(@event.BookingId, nameof(Booking));
            }
            //delete booking
            await _repository.DeleteAsync(@event.BookingId);
        }
    }
}
