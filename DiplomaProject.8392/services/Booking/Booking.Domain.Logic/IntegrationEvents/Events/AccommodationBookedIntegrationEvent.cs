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
        public long BookingId { get; private set; }
        public long AccommodationId { get; private set; }
        public long GuestId { get;private set; }
        public long OwnerId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public AccommodationBookedIntegrationEvent(
            long bookingId,
            long accommodationId,
            long guestId,
            long ownerId,
            DateTime startDate,
            DateTime endDate)
        {
            BookingId = bookingId;
            AccommodationId = accommodationId;
            GuestId = guestId;
            OwnerId = ownerId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
