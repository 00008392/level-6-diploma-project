using Profile.Domain.Entities;
using Profile.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile.Domain.Logic.DTOs
{
    public class ProfileInfoDTO: BaseProfileDTO
    {

        public DateTime RegistrationDate { get; private set; }
        public CityDTO City { get; private set; }
        public CountryDTO Country { get; private set; }
        public byte[] ProfilePhoto { get;private set; }
        public string MimeType { get;private set; }
        public ProfileInfoDTO(long id, string firstName,
    string lastName, string email,
    string phoneNumber, DateTime? dateOfBirth,
    Gender? gender, string address,
    string userInfo, DateTime registrationDate, CityDTO city,
    CountryDTO country,
    byte[] profilePhoto, string mimeType) : base(id, firstName,
        lastName, email, phoneNumber,
        dateOfBirth, gender,
        address, userInfo)
        {
            RegistrationDate = registrationDate;
            City = city;
            Country = country;
            ProfilePhoto = profilePhoto;
            MimeType = mimeType;
        }
    }
}
