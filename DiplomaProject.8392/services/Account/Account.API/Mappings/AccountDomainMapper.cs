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
                .ForMember(u => u.Role, opt => opt.MapFrom(src => (int)src.Role));
        }
    }
}
