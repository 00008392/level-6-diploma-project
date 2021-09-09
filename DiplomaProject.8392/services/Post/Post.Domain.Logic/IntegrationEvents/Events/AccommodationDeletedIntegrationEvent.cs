using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBus.Events;

namespace Post.Domain.Logic.IntegrationEvents.Events
{
    public class AccommodationDeletedIntegrationEvent: IntegrationEvent
    {
        public long AccommodationId { get; }

        public AccommodationDeletedIntegrationEvent(long accommodationId)
        {
            AccommodationId = accommodationId;
        }
    }
}
