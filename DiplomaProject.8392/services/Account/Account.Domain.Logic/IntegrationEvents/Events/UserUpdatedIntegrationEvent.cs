using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.IntegrationEvents.Events
{
   public class UserUpdatedIntegrationEvent
    {
        public long UserId { get; set; }
        public string Email { get; set; }
    }
}
