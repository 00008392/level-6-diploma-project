using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.IntegrationEvents.Events
{
    //event raised when booking is accepted by accommodation owner (booking status changed to Accepted)
    //published by this microservice thhrough event bus
    public class BookingAcceptedIntegrationEvent: IntegrationEvent
    {
        public long BookingId { get; private set; }
        public long PostId { get; private set; }
        public long GuestId { get;private set; }
        public long OwnerId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public BookingAcceptedIntegrationEvent(
            long bookingId,
            long postId,
            long guestId,
            long ownerId,
            DateTime startDate,
            DateTime endDate)
        {
            BookingId = bookingId;
            PostId = postId;
            GuestId = guestId;
            OwnerId = ownerId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
