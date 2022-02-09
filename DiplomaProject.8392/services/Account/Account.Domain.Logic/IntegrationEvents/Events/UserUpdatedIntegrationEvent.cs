using Account.Domain.Enums;
using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.IntegrationEvents.Events
{
    //this event is published by account microservice through event bus when user information is updated
    public class UserUpdatedIntegrationEvent: IntegrationEvent
    {
        public long UserId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public int Gender { get; private set; }
        public string Address { get; private set; }
        public string UserInfo { get; private set; }
        public long CountryId { get; private set; }

        public UserUpdatedIntegrationEvent(
            long userId,
            string firstName,
            string lastName,
            string email,
            string phoneNumber,
            DateTime dateOfBirth,
            int gender,
            string address,
            string userInfo,
            long countryId)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Address = address;
            UserInfo = userInfo;
            CountryId = countryId;
        }
    }
}
