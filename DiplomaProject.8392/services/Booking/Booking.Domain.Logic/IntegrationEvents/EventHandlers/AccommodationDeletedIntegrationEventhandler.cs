using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Domain.Entities;
using Booking.Domain.Logic.Contracts;
using Booking.Domain.Logic.IntegrationEvents.EventHandlers.Core;
using Booking.Domain.Logic.IntegrationEvents.Events;
using EventBus.Contracts;

namespace Booking.Domain.Logic.IntegrationEvents.EventHandlers
{
    class AccommodationDeletedIntegrationEventhandler : BaseIntegrationEventHandler<Accommodation>,
        IIntegrationEventHandler<AccommodationDeletedIntegrationEvent>
    {
        public AccommodationDeletedIntegrationEventhandler(IEventHandlerService<Accommodation> service)
            : base(service)
        {
        }

        public async Task Handle(AccommodationDeletedIntegrationEvent @event)
        {
            await _service.DeleteEntity(@event.AccommodationId);
        }
    }
}
