using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBus.Events;

namespace Profile.Domain.Logic.IntegrationEvents.Events
{
    public class UserDeletedIntegrationEvent: IntegrationEvent
    {
        public long UserId { get; set; }
    }
}
