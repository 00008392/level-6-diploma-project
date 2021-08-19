using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.IntegrationEvents.Events
{
    public class UserCreatedIntegrationEvent
    {
        public string Email { get; set; }
    }
}
