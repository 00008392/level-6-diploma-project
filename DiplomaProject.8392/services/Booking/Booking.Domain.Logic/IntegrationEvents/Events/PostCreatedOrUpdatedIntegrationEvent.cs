using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.IntegrationEvents.Events
{
    //event raised by post microservice when new post is created or existing post is updated
    //consumed by this microservice through event bus
    public class PostCreatedOrUpdatedIntegrationEvent: IntegrationEvent
    {
        public long PostId { get; private set; }
        public long OwnerId { get; private set; }
        public int MaxGuestsNo { get; private set; }

        public PostCreatedOrUpdatedIntegrationEvent(
            long postId,
            long ownerId,
            int maxGuestsNo)
        {
            PostId = postId;
            OwnerId = ownerId;
            MaxGuestsNo = maxGuestsNo;
        }
    }
}
