
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBus.Events;

namespace PostFeedback.Domain.Logic.IntegrationEvents.Events
{
    //this event is published by account microservice
    //when user information is updated and consumed by post microservice
    public class UserUpdatedIntegrationEvent: IntegrationEvent
    {
        public long UserId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }

        public UserUpdatedIntegrationEvent(
            long userId,
            string firstName,
            string lastName,
            string email,
            string phoneNumber)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}
