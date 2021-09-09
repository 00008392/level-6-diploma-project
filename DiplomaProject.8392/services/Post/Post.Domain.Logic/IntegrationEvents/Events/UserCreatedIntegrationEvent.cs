using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBus.Events;

namespace Post.Domain.Logic.IntegrationEvents.Events
{
    public class UserCreatedIntegrationEvent: IntegrationEvent
    {
        public string Email { get; }

        public UserCreatedIntegrationEvent(string email)
        {
            Email = email;
        }
    }
}
