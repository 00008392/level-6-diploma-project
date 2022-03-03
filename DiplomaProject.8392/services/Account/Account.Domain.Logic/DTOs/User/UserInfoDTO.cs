using Account.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.DTOs
{
    //dto for holding user information
    public class UserInfoDTO: UserBaseDTO
    {
        public long Id { get; private set; }
        public string PhoneNumber { get; private set; }
        public string UserInfo { get; private set; }
        public DateTime RegistrationDate { get; private set; }
        public string CountryName { get; private set; }
        public UserInfoDTO(
            long id,
            string firstName,
            string lastName,
            string email,
            string phoneNumber,
            DateTime? dateOfBirth,
            Gender? gender,
            string userInfo,
            DateTime registrationDate,
            string countryName,
            long countryId) : base(
                firstName,
                lastName,
                email,
                dateOfBirth,
                gender,
                countryId)
        {
            Id = id;
            PhoneNumber = phoneNumber;
            RegistrationDate = registrationDate;
            CountryName = countryName;
            UserInfo = userInfo;
        }
    }
}
