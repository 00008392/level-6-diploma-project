using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.IntegrationEvents.Events
{
    //tested
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
