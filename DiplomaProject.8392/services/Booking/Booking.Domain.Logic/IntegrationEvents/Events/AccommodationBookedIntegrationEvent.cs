using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.IntegrationEvents.Events
{
    public class AccommodationBookedIntegrationEvent: IntegrationEvent
    {
        public long AccommodationId { get; private set; }
        public long BookingId { get; private set; }
        public long GuestId { get; set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public AccommodationBookedIntegrationEvent(
            long accommodationId,
            long bookingId,
            long guestId,
            DateTime startDate,
            DateTime endDate)
        {
            AccommodationId = accommodationId;
            BookingId = bookingId;
            GuestId = guestId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
