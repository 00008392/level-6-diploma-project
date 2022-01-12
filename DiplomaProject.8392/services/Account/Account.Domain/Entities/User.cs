
using Account.Domain.Enums;
using BaseClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Entities
{
   public class User: BaseEntity
    {
        public string Email { get; private set; }
        public DateTime RegistrationDate { get; private set; }
        public Role Role { get; private set; }
        public string PasswordHash { get; private set; }
        public string PasswordSalt { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string PhoneNumber { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public Gender Gender { get; private set; }
        public string Address { get; private set; }
        public long? CountryId { get; private set; }
        public Country Country { get; private set; }
        public string UserInfo { get; private set; }
        public byte[] ProfilePhoto { get; private set; }
        public string MimeType { get; private set; }
        public ICollection<Booking> BookingsAsGuest { get; }
        public ICollection<Booking> BookingsAsOwner { get; }

        //registration constructor
        public User(string email, DateTime registrationDate,
            Role role, string firstName,
            string lastName, DateTime dateOfBirth, 
            Gender gender)
        {
            Email = email;
            RegistrationDate = registrationDate;
            Role = role;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
        }

        //for registration and password change
        public void SetPassword(string passwordHash, string passwordSalt)
        {
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }
        public void UpdateInfo(string firstName,
           string lastName,
           string email, string phoneNumber,
           DateTime dateOfBirth,
           Gender gender, string address,
           long? countryId, string userInfo)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Address = address;
            CountryId = countryId;
            UserInfo = userInfo;
        }
    }
}
