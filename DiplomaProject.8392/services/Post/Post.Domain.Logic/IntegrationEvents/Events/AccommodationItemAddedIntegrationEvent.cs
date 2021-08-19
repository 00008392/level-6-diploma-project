using Post.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.IntegrationEvents.Events
{
    public class AccommodationItemAddedIntegrationEvent<T>
        where T: ItemAccommodationBase
    {
        public long AccommodationId { get; set; }
        public long ItemId { get; set; }   
        public string OtherItem { get; set; }
    }
}
