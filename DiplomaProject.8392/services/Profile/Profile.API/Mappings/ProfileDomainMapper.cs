using AutoMapper;
using Profile.Domain.Entities;
using Profile.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Profile.API.Mappings
{
    public class ProfileDomainMapper: AutoMapper.Profile
    {
        public ProfileDomainMapper()
        {
            CreateMap<CreateProfileDTO, User>()
                .ConvertUsing(x => new User(x.Email, x.RegistrationDate));
            CreateMap<Domain.Entities.City, CityDTO>()
                .ConvertUsing(x => new CityDTO(x.Id, x.Name));
            CreateMap<Domain.Entities.Country, CountryDTO>()
                .ConvertUsing(x => new CountryDTO(x.Id, x.Name));
            CreateMap<User, ProfileInfoDTO>()
                .ConvertUsing((x, dest,context) => new ProfileInfoDTO(x.Id, x.FirstName,
                x.LastName, x.Email, x.PhoneNumber, x.DateOfBirth, x.Gender,
                x.Address, x.UserInfo, x.RegistrationDate, x.City == null ? null : context.Mapper.Map<CityDTO>(x.City),
                x.City == null ? null : context.Mapper.Map<CountryDTO>(x.City.Country), x.ProfilePhoto ==null?null:x.ProfilePhoto,
                x.MimeType));

        }
     
    }
}
