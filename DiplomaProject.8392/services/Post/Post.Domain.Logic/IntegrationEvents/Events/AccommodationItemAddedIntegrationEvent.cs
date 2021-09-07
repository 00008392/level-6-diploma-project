using Post.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBus.Events;

namespace Post.Domain.Logic.IntegrationEvents.Events
{
    public class AccommodationItemAddedIntegrationEvent<T>: IntegrationEvent
        where T: ItemAccommodationBase
    {
        public long AccommodationId { get; set; }
        public long ItemId { get; set; }   
        public string OtherItem { get; set; }
    }
}
