using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.IntegrationEvents.Events
{
    //this event is published by booking microservice when booking request on accommodation
    //is cancelled and consumed by this microservice
    public class BookingCancelledIntegrationEvent: IntegrationEvent
    {
        public long BookingId { get; private set; }

        public BookingCancelledIntegrationEvent(long bookingId)
        {
            BookingId = bookingId;
        }
    }
}
