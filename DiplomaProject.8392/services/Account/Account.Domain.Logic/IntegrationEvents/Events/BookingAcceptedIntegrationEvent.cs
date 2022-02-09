using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.IntegrationEvents.Events
{
    //this event is published by booking microservice when booking request on accommodation
    //is accepted and consumed by this microservice 
    public class BookingAcceptedIntegrationEvent : IntegrationEvent
    {
        public long BookingId { get; private set; }
        //account microservice does not need to know which accommodation was booked,
        //but it needs to know who is the owner of this accommodation
        public long OwnerId { get; private set; }
        public long GuestId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public BookingAcceptedIntegrationEvent(
            long bookingId,
            long ownerId,
            long guestId,
            DateTime startDate,
            DateTime endDate)
        {
            BookingId = bookingId;
            OwnerId = ownerId;
            GuestId = guestId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
