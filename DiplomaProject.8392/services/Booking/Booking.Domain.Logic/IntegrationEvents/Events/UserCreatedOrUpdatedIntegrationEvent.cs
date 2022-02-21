using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.IntegrationEvents.Events
{
    //event raised by account microservice when new user is created or existing user is updated
    //consumed by this microservice through event bus
    public class UserCreatedOrUpdatedIntegrationEvent : IntegrationEvent
    {
        public long UserId { get;private set; }

        public UserCreatedOrUpdatedIntegrationEvent(long userId)
        {
            UserId = userId;
        }
    }
}
