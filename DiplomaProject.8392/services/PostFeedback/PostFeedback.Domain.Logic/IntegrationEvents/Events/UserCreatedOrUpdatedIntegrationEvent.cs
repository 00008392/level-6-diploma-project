
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBus.Events;

namespace PostFeedback.Domain.Logic.IntegrationEvents.Events
{
    //this event is consumed by post microservice and published by account microservice
    //when new user is registered or when user information is updated 
    public class UserCreatedOrUpdatedIntegrationEvent: IntegrationEvent
    {
        public long UserId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }

        public UserCreatedOrUpdatedIntegrationEvent(
            long userId,
            string firstName,
            string lastName,
            string email)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}
