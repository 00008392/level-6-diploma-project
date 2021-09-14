using Account.Domain.Enums;
using Account.Domain.Logic.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account.API.Mappings
{
    public class AccountGrpcMapper: Profile
    {
        public AccountGrpcMapper()
        {
            CreateMap<LoggedUserDTO, LoginReply>()
                .ForMember(u => u.NoUser, opt => opt.Ignore());
            CreateMap<LoginRequest, UserLoginDTO>();
            CreateMap<RegistrationRequest, UserRegistrationDTO>()
                .ForMember(u => u.Role, opt => opt.MapFrom(src => (Role?)src.Role));
            CreateMap<ChangePasswordRequest, ChangePasswordDTO>();
        }
    }
}
