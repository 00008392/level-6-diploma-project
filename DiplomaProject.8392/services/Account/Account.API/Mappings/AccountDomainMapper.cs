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
        //class for mapping domain entities to dtos and vice versa
        public AccountDomainMapper()
        {
            //user
            CreateMap<User, LoggedUserDTO>()
                .ConvertUsing(x => new LoggedUserDTO(x.Id, x.Email));
            CreateMap<User, UserInfoDTO>()
                .ConvertUsing((x, dest, context) => new UserInfoDTO(x.Id, x.FirstName, x.LastName, x.Email, x.PhoneNumber,
                x.DateOfBirth, x.Gender, x.UserInfo, x.RegistrationDate, x.Country?.Name, x.Country?.Id ?? 0));
            CreateMap<UserRegistrationDTO, User>()
                .ConvertUsing(x => new User(x.FirstName, x.LastName, x.Email, DateTime.Now,
                (DateTime)x.DateOfBirth, (Gender)x.Gender, x.CountryId));
            //country
            CreateMap<Domain.Entities.Country, CountryDTO>()
                .ConvertUsing(x => new CountryDTO(x.Id, x.Name));

        }
    }
}
