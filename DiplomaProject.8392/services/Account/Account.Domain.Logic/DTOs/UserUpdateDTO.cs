using Account.Domain.Enums;
using Account.Domain.Logic.DTOs.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.DTOs
{
    public class UserUpdateDTO: UserBaseDTO
    {
        public long Id { get; private set; }
        public string PhoneNumber { get; private set; }
        public long? CountryId { get; private set; }
        public string Address { get; private set; }
        public string UserInfo { get; private set; }
        public UserUpdateDTO(long id, string firstName,
         string lastName, string email,
         string phoneNumber, DateTime? dateOfBirth,
         Gender? gender, string address,
         string userInfo, long? countryId) :base(firstName, lastName, 
             email, dateOfBirth, gender)
        {
            Id = id;
            PhoneNumber = phoneNumber;
            CountryId = countryId;
            Address = address;
            UserInfo = userInfo;
        }
    }
}
