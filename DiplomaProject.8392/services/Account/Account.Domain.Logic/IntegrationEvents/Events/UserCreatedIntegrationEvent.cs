using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.IntegrationEvents.Events
{
    public class UserCreatedIntegrationEvent: IntegrationEvent
    {
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
