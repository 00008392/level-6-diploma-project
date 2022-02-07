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
        public string Address { get; private set; }
        public string UserInfo { get; private set; }
        public DateTime RegistrationDate { get; private set; }
        public string CountryName { get; private set; }
        public byte[] ProfilePhoto { get; private set; }
        public string MimeType { get; private set; }
        public UserInfoDTO(
            long id,
            string firstName,
            string lastName,
            string email,
            string phoneNumber,
            DateTime? dateOfBirth,
            Gender? gender,
            string address,
            string userInfo,
            DateTime registrationDate,
            string countryName,
            long countryId,
            byte[] profilePhoto,
            string mimeType) : base(
                firstName,
                lastName,
                email,
                dateOfBirth,
                gender,
                countryId)
        {
            Id = id;
            PhoneNumber = phoneNumber;
            Address = address;
            RegistrationDate = registrationDate;
            CountryName = countryName;
            UserInfo = userInfo;
            ProfilePhoto = profilePhoto;
            MimeType = mimeType;
        }
    }
}
