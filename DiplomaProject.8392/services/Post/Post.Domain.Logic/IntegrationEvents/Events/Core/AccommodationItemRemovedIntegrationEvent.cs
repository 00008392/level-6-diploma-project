using Post.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBus.Events;

namespace Post.Domain.Logic.IntegrationEvents.Events.Core
{
   public abstract class AccommodationItemRemovedIntegrationEvent: IntegrationEvent
    {
        public ICollection<long> ItemAccommodationIds { get; protected set; }

        public AccommodationItemRemovedIntegrationEvent(ICollection<long> itemAccommodationIds)
        {
            ItemAccommodationIds = itemAccommodationIds;
        }
    }
}
