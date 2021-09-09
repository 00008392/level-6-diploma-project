using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.IntegrationEvents.Events
{
   public class UserUpdatedIntegrationEvent: IntegrationEvent
    {
        public long UserId { get;}
        public string Email { get; }

        public UserUpdatedIntegrationEvent(long userId, string email)
        {
            UserId = userId;
            Email = email;
        }
    }
}
