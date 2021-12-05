using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.IntegrationEvents.Events
{
   public class AccommodationBookingCancelledIntegrationEvent: IntegrationEvent
    {
        public long BookingId { get;private set; }

        public AccommodationBookingCancelledIntegrationEvent(long bookingId)
        {
            BookingId = bookingId;
        }
    }
}
