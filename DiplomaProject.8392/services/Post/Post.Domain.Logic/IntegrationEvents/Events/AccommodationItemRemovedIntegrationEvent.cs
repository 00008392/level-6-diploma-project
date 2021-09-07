using Post.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBus.Events;

namespace Post.Domain.Logic.IntegrationEvents.Events
{
   public class AccommodationItemRemovedIntegrationEvent<T>: IntegrationEvent
        where T: ItemAccommodationBase
    {
        public long ItemAccommodationId { get; set; }
    }
}
