using Account.Domain.Logic.Contracts;
using Account.Domain.Logic.IntegrationEvents.EventHandlers.Core;
using Account.Domain.Logic.IntegrationEvents.Events;
using AutoMapper;
using EventBus.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.IntegrationEvents.EventHandlers
{
    public class AccommodationBookingCancelledIntegrationEventHandler : BaseIntegrationEventHandler,
        IIntegrationEventHandler<AccommodationBookingCancelledIntegrationEvent>
    {
        public AccommodationBookingCancelledIntegrationEventHandler(
          IEventHandlerService service) : base(service)
        {
        }
        public async Task Handle(AccommodationBookingCancelledIntegrationEvent @event)
        {
            await _service.DeleteBooking(@event.BookingId);
        }
    }
}
