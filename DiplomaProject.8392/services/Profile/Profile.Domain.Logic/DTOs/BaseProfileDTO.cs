using Profile.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile.Domain.Logic.DTOs
{
   public class BaseProfileDTO
    {
        public long Id { get;protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Email { get; protected set; }
        public string PhoneNumber { get; protected set; }
        public DateTime? DateOfBirth { get; protected set; }
        public Gender? Gender { get; protected set; }
        public string Address { get; protected set; }
        public string UserInfo { get; protected set; }

        protected BaseProfileDTO(long id, string firstName, 
            string lastName, string email,
            string phoneNumber, DateTime? dateOfBirth, 
            Gender? gender, string address,
            string userInfo)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Address = address;
            UserInfo = userInfo;
        }
    }
}
