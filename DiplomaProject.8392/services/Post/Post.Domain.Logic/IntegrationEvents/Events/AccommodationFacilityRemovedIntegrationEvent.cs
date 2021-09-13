﻿using Post.Domain.Logic.IntegrationEvents.Events.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.IntegrationEvents.Events
{
    public class AccommodationFacilityRemovedIntegrationEvent: AccommodationItemRemovedIntegrationEvent
    {
        public AccommodationFacilityRemovedIntegrationEvent(ICollection<long> itemAccommodationIds) 
            : base(itemAccommodationIds)
        {

        }
    }
}