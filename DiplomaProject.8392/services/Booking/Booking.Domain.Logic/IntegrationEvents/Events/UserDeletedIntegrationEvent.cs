using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.IntegrationEvents.Events
{
    //event raised by account microservice when user is deleted
    //consumed by this microservice through event bus
    public class UserDeletedIntegrationEvent: IntegrationEvent
    {
        public long UserId { get; private set; }

        public UserDeletedIntegrationEvent(long userId)
        {
            UserId = userId;
        }
    }
}
