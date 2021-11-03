using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.IntegrationEvents.Events
{
    //tested
    public class AccommodationDeletedIntegrationEvent : IntegrationEvent
    {
        public long AccommodationId { get; private set; }

        public AccommodationDeletedIntegrationEvent(long accommodationId)
        {
            AccommodationId = accommodationId;
        }
    }
}
