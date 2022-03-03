using Account.Domain.Enums;
using DAL.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Entities
{
    //domain entity for account
   public class User: BaseEntity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public DateTime RegistrationDate { get; private set; }
        public string PhoneNumber { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public Gender Gender { get; private set; }
        public long CountryId { get; private set; }
        public Country Country { get; private set; }
        public string UserInfo { get; private set; }
        //bookings that user has on accommodations posted by him/her
        public ICollection<Booking> BookingsAsOwner { get; private set; }
        //bookings that user has on accommodations as guest
        public ICollection<Booking> BookingsAsGuest { get; private set; }
        //password hash and salt are used for password encryption
        public string PasswordHash { get; private set; }
        public string PasswordSalt { get; private set; }

        //constructor used for account registration, takes basic required user information
        public User(
            string firstName,
            string lastName,
            string email,
            DateTime registrationDate,
            DateTime dateOfBirth,
            Gender gender,
            long countryId)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            RegistrationDate = registrationDate;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            CountryId = countryId;
        }

        //method which is called during account registraton and password change
        public void SetPassword(
            string passwordHash,
            string passwordSalt)
        {
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }
        //method for updating account, takes additional user information
        public void UpdateInfo(
            string firstName,
            string lastName,
            string email,
            string phoneNumber,
            DateTime dateOfBirth,
            Gender gender,
            long countryId,
            string userInfo)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            CountryId = countryId;
            UserInfo = userInfo;
        }
    }
}
