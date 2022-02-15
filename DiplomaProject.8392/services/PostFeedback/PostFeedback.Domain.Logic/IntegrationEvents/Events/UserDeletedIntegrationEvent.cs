using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBus.Events;

namespace PostFeedback.Domain.Logic.IntegrationEvents.Events
{
    //this event is published by account microservice
    //when user is deleted and consumed by post microservice
    public class UserDeletedIntegrationEvent: IntegrationEvent
    {
        public long UserId { get; private set; }

        public UserDeletedIntegrationEvent(long userId)
        {
            UserId = userId;
        }
    }
}
