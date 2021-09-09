using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.IntegrationEvents.Events
{
   public class UserDeletedIntegrationEvent: IntegrationEvent
    {
        public long UserId { get; }
        public UserDeletedIntegrationEvent(long userId)
        {
            UserId = userId;
        }
    }
}
