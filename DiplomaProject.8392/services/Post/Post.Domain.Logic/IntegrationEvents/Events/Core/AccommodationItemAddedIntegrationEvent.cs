using Post.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBus.Events;

namespace Post.Domain.Logic.IntegrationEvents.Events.Core
{
    public abstract class AccommodationItemAddedIntegrationEvent: IntegrationEvent
     
    {
        public ICollection<AccommodationItemEventBase> AccommodationItems { get; }

        public AccommodationItemAddedIntegrationEvent(ICollection<AccommodationItemEventBase>
            accommodationItems)
        {
            AccommodationItems = accommodationItems;
        }
    }

    public class AccommodationItemEventBase
    {
        public long AccommodationId { get; }
        public long ItemId { get; }
        public string OtherItem { get; }

        public AccommodationItemEventBase(long accommodationId, long itemId, string otherItem)
        {
            AccommodationId = accommodationId;
            ItemId = itemId;
            OtherItem = otherItem;
        }
    }
}
