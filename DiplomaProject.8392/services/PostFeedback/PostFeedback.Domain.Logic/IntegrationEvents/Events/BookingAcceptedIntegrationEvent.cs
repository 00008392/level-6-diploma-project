using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.IntegrationEvents.Events
{
    //this event is published by booking microservice when booking request on accommodation
    //is accepted and consumed by this microservice 
    public class BookingAcceptedIntegrationEvent: IntegrationEvent
    {
        public long BookingId { get; private set; }
        public long PostId { get; private set; }
        public long GuestId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public BookingAcceptedIntegrationEvent(
            long bookingId,
            long postId,
            long guestId,
            DateTime startDate,
            DateTime endDate)
        {
            BookingId = bookingId;
            PostId = postId;
            GuestId = guestId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
