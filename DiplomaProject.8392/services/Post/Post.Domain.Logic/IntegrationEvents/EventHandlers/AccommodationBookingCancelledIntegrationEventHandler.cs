using AutoMapper;
using EventBus.Contracts;
using Post.Domain.Logic.Contracts;
using Post.Domain.Logic.IntegrationEvents.EventHandlers.Core;
using Post.Domain.Logic.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.IntegrationEvents.EventHandlers
{
    public class AccommodationBookingCancelledIntegrationEventHandler : BaseIntegrationEventHandler,
        IIntegrationEventHandler<AccommodationBookingCancelledIntegrationEvent>
    {
        public AccommodationBookingCancelledIntegrationEventHandler(IEventHandlerService service,
                                                                    IMapper mapper) : base(service, mapper)
        {
        }

        public async Task Handle(AccommodationBookingCancelledIntegrationEvent @event)
        {
            await _service.RemoveBookingAsync(@event.BookingId);
        }
    }
}
