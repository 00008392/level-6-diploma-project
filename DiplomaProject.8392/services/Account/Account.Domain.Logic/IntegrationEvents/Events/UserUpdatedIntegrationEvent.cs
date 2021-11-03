using Account.Domain.Enums;
using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.IntegrationEvents.Events
{
    //tested
   public class UserUpdatedIntegrationEvent: IntegrationEvent
    {
        public long UserId { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string PhoneNumber { get; }
        public DateTime DateOfBirth { get; }
        public int Gender { get; }
        public string Address { get; }
        public string UserInfo { get; }
        public long? CityId { get; }

        public UserUpdatedIntegrationEvent(long userId, string firstName,
            string lastName, string email,
            string phoneNumber, DateTime dateOfBirth,
            int gender, string address,
            string userInfo, long? cityId)
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
            CityId = cityId;
        }
    }
}
