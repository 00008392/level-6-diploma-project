
using BaseClasses.Entities;
using Profile.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile.Domain.Entities
{
    public class User: BaseEntity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public DateTime? DateOfBirth { get; private set; }
        public DateTime RegistrationDate { get; private set; }
        public Gender? Gender { get; private set; }
        public string Address { get; private set; }
        public long? CityId { get; private set; }
        public City City { get; }
        public string UserInfo { get; private set; }
        public byte[] ProfilePhoto { get; private set; }
        public string MimeType { get; private set; }

        public User(string email, DateTime registrationDate)
        {
            Email = email;
            RegistrationDate = registrationDate;
        }

        public void UpdateInfo(string firstName,
            string lastName,
            string email, string phoneNumber,
            DateTime? dateOfBirth,
            Gender? gender, string address,
            long? cityId, string userInfo)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Address = address;
            CityId = cityId;
            UserInfo = userInfo;
        }
    }
}
