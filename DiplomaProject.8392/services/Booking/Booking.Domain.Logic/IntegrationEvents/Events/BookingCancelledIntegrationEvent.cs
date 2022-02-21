using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.IntegrationEvents.Events
{
    //event raised when accepted booking is cancelled 
    //published by this microservice thhrough event bus
    public class BookingCancelledIntegrationEvent: IntegrationEvent
    {
        public long BookingId { get;private set; }

        public BookingCancelledIntegrationEvent(long bookingId)
        {
            BookingId = bookingId;
        }
    }
}
