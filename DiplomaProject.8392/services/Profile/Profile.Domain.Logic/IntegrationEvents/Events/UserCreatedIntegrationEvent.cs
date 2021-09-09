using Profile.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBus.Events;

namespace Profile.Domain.Logic.IntegrationEvents.Events
{
   public class UserCreatedIntegrationEvent: IntegrationEvent
    {
        public string Email { get; }
        public DateTime RegistrationDate { get; }

        public UserCreatedIntegrationEvent(string email, DateTime registrationDate)
        {
            Email = email;
            RegistrationDate = registrationDate;
        }

    }
}
