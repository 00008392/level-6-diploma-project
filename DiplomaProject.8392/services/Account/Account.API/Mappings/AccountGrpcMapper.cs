using Account.Domain.Enums;
using Account.Domain.Logic.DTOs;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Base.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account.API.Mappings
{
    public class AccountGrpcMapper: Profile
    {
        //class for mapping grpc generated classes to dtos and vice versa
        public AccountGrpcMapper()
        {
            //user
            CreateMap<LoggedUserDTO, LoginResponse>()
                .ForMember(u => u.NoUser, opt => opt.Ignore());
            CreateMap<LoginRequest, UserLoginDTO>()
                .ConvertUsing(x => new UserLoginDTO(x.Password, x.Email));
            CreateMap<RegisterRequest, UserRegistrationDTO>()
                .ConvertUsing((x, context) => new UserRegistrationDTO(x.Email, x.FirstName, x.LastName, x.DateOfBirthTimeStamp?.ToDateTime(),
                (Gender?)x.Gender, x.CountryId??0, x.Password));
            CreateMap<ChangePasswordRequest, ChangePasswordDTO>()
                .ConvertUsing(x => new ChangePasswordDTO(x.Id, x.Password));
            CreateMap<UpdateRequest, UserUpdateDTO>()
                .ConvertUsing((x, context) => new UserUpdateDTO(x.Id, x.FirstName,
                x.LastName, x.Email, x.PhoneNumber, x.DateOfBirthTimeStamp?.ToDateTime(), (Gender?)x.Gender, x.UserInfo, x.CountryId??0));
            CreateMap<UserInfoDTO, UserInfoResponse>()
                 .ForMember(x => x.DateOfBirthTimeStamp, opt => opt.MapFrom(src => GrpcServiceHelper.ConvertDateTimeToTimeStamp(src.DateOfBirth)))
                 .ForMember(x => x.RegistrationDateTimeStamp, opt => opt.MapFrom(src =>
                     GrpcServiceHelper.ConvertDateTimeToTimeStamp(src.RegistrationDate)));
            //country
            CreateMap<CountryDTO, Country>();
        }
    }
}
