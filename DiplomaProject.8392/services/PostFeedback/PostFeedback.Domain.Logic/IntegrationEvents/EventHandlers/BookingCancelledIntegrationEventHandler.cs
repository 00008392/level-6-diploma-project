using AutoMapper;
using EventBus.Contracts;
using PostFeedback.Domain.Logic.Contracts;
using PostFeedback.Domain.Logic.IntegrationEvents.EventHandlers.Core;
using PostFeedback.Domain.Logic.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.IntegrationEvents.EventHandlers
{
    public class BookingCancelledIntegrationEventHandler : BaseIntegrationEventHandler,
        IIntegrationEventHandler<BookingCancelledIntegrationEvent>
    {
        public BookingCancelledIntegrationEventHandler(IEventHandlerService service,
                                                                    IMapper mapper) : base(service, mapper)
        {
        }

        public async Task Handle(BookingCancelledIntegrationEvent @event)
        {
            await _service.RemoveBookingAsync(@event.BookingId);
        }
    }
}
