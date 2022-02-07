using Account.Domain.Enums;
using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.IntegrationEvents.Events
{
    //this event is published by account microservice through event bus when new user is registered
    public class UserCreatedIntegrationEvent: IntegrationEvent
    {
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public int Gender { get; private set; }
        public DateTime RegistrationDate { get; private set; }
        public long CountryId { get; private set; }

        public UserCreatedIntegrationEvent(
            string email,
            string firstName,
            string lastName,
            DateTime dateOfBirth,
            int gender,
            DateTime registrationDate,
            long countryId)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            RegistrationDate = registrationDate;
            CountryId = countryId;
        }
    }
}
