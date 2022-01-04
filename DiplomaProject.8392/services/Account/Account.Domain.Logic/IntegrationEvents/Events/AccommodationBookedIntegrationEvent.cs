using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.IntegrationEvents.Events
{
    public class AccommodationBookedIntegrationEvent : IntegrationEvent
    {
        public long BookingId { get; private set; }
        public long GuestId { get; private set; }
        public long OwnerId { get; private set; }

        public AccommodationBookedIntegrationEvent(
            long bookingId,
            long guestId,
            long ownerId)
        {
            BookingId = bookingId;
            GuestId = guestId;
            OwnerId = ownerId;
        }
    }
}
