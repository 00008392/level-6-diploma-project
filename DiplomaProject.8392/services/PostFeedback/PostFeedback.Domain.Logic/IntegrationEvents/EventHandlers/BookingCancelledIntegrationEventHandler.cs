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
    //event handler that reacts when booking is cancelled
    public class BookingCancelledIntegrationEventHandler : BaseIntegrationEventHandler,
        IIntegrationEventHandler<BookingCancelledIntegrationEvent>
    {
        public BookingCancelledIntegrationEventHandler(
            IEventHandlerService service,
            IMapper mapper) : base(
                service,
                mapper)
        {
        }

        public async Task Handle(BookingCancelledIntegrationEvent @event)
        {
            //if booking is cancelled, call service to remove booking from this microservice
            await _service.RemoveBookingAsync(@event.BookingId);
        }
    }
}
