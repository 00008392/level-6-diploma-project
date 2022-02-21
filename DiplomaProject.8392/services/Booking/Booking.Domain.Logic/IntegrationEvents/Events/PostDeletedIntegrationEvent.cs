using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.IntegrationEvents.Events
{
    //event raised by post microservice when post is deleted
    //consumed by this microservice through event bus
    public class PostDeletedIntegrationEvent : IntegrationEvent
    {
        public long PostId { get; private set; }

        public PostDeletedIntegrationEvent(long postId)
        {
            PostId = postId;
        }
    }
}
