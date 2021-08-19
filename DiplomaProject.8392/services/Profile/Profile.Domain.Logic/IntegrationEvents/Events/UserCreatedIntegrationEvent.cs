using Profile.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile.Domain.Logic.IntegrationEvents.Events
{
   public class UserCreatedIntegrationEvent
    {
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
