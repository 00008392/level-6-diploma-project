using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.IntegrationEvents.Events
{
    //tested
   public class UserCreatedIntegrationEvent: IntegrationEvent
    {
        public string Email { get; private set; }

        public UserCreatedIntegrationEvent(string email)
        {
            Email = email;
        }
    }
}
