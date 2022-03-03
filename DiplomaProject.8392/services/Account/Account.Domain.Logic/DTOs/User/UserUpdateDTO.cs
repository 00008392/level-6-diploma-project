using Account.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.DTOs
{
    //dto for updating user information
    public class UserUpdateDTO: UserBaseDTO
    {
        public long Id { get; private set; }
        public string PhoneNumber { get; private set; }
        public string UserInfo { get; private set; }
        public UserUpdateDTO(
            long id,
            string firstName,
            string lastName,
            string email,
            string phoneNumber,
            DateTime? dateOfBirth,
            Gender? gender,
            string userInfo,
            long countryId) :base(
                firstName,
                lastName,
                email,
                dateOfBirth,
                gender,
                countryId)
        {
            Id = id;
            PhoneNumber = phoneNumber;
            UserInfo = userInfo;
        }
    }
}
