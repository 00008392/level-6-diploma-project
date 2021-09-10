
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBus.Events;

namespace Post.Domain.Logic.IntegrationEvents.Events
{
    //tested
   public class UserUpdatedIntegrationEvent: IntegrationEvent
    {
        public long UserId { get;}
        public string FirstName { get; }
        public string LastName { get;  }
        public string Email { get; }
        public string PhoneNumber { get; }

        public UserUpdatedIntegrationEvent(long userId, string firstName, 
            string lastName, string email, 
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
