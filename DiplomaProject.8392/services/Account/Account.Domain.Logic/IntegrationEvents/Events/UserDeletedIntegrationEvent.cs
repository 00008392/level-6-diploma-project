using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.IntegrationEvents.Events
{
    //this event is published by account microservice through event bus when user is deleted
    public class UserDeletedIntegrationEvent: IntegrationEvent
    {
        public long UserId { get; private set; }
        public UserDeletedIntegrationEvent(long userId)
        {
            UserId = userId;
        }
    }
}
