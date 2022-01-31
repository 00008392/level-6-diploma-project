using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBus.Events;

namespace PostFeedback.Domain.Logic.IntegrationEvents.Events
{
    //tested
    public class PostDeletedIntegrationEvent: IntegrationEvent
    {
        public long PostId { get; }

        public PostDeletedIntegrationEvent(long postId)
        {
            PostId = postId;
        }
    }
}
