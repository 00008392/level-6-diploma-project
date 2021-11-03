using Account.Domain.Entities;
using Account.Domain.Enums;
using Account.Domain.Logic.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.API.Mappings
{
   public class AccountDomainMapper: Profile
    {
        public AccountDomainMapper()
        {
            CreateMap<User, LoggedUserDTO>()
                .ConvertUsing(x => new LoggedUserDTO(x.Id, x.Email, (int)x.Role));
            CreateMap<Domain.Entities.City, CityDTO>()
                .ConvertUsing(x => new CityDTO(x.Id, x.Name));
            CreateMap<Domain.Entities.Country, CountryDTO>()
                .ConvertUsing(x => new CountryDTO(x.Id, x.Name));
            CreateMap<User, UserInfoDTO>()
                .ConvertUsing((x, dest, context) => new UserInfoDTO(x.Id, x.FirstName, x.LastName,
                x.Email, x.PhoneNumber, x.DateOfBirth, x.Gender, x.Address,
                x.UserInfo, x.RegistrationDate, x.City == null ? null : context.Mapper.Map<CityDTO>(x.City),
                x.City == null ? null : context.Mapper.Map<CountryDTO>(x.City.Country),
                x.ProfilePhoto == null ? null : x.ProfilePhoto, x.MimeType));
            CreateMap<UserRegistrationDTO, User>()
                .ConvertUsing(x => new User(x.Email, DateTime.Now, (Role)x.Role, x.FirstName,
                x.LastName, (DateTime)x.DateOfBirth, (Gender)x.Gender));


        }
    }
}
