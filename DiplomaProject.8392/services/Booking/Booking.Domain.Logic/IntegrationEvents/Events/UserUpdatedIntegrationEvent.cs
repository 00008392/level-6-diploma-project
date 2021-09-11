using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.IntegrationEvents.Events
{
    public class UserUpdatedIntegrationEvent : IntegrationEvent
    {
        public long UserId { get;private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Address { get; private set; }
        public DateTime? DateOfBirth { get; private set; }

        public UserUpdatedIntegrationEvent(long userId, string firstName,
            string lastName, string email, string phoneNumber, 
            string address, DateTime? dateOfBirth)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            DateOfBirth = dateOfBirth;
        }
    }
}
