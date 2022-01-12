using Account.Domain.Enums;
using Account.Domain.Logic.DTOs;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;
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
            CreateMap<LoginRequest, UserLoginDTO>()
                .ConvertUsing(x => new UserLoginDTO(x.Password, x.Email));
            CreateMap<RegistrationRequest, UserRegistrationDTO>()
                .ConvertUsing((x, context) => new UserRegistrationDTO(x.Email, (Role?)x.Role, x.FirstName, x.LastName,
                x.DateOfBirthTimeStamp?.ToDateTime(), (Gender?)x.Gender, x.Password));
            CreateMap<ChangePasswordRequest, ChangePasswordDTO>()
                .ConvertUsing(x => new ChangePasswordDTO(x.Id, x.Password));
            CreateMap<UpdateRequest, UserUpdateDTO>()
                .ConvertUsing((x, context) => new UserUpdateDTO(x.Id, x.FirstName,
                x.LastName, x.Email, x.PhoneNumber, x.DateOfBirthTimeStamp?.ToDateTime(), (Gender?)x.Gender,
                x.Address, x.UserInfo, x.CountryId));
            CreateMap<CountryDTO, Country>();
            CreateMap<UserInfoDTO, UserInfoResponse>()
                 .ForMember(x => x.DateOfBirthTimeStamp, opt => opt.MapFrom(src => FromDateTimeToTimeStamp(src.DateOfBirth)))
                 .ForMember(x => x.RegistrationDateTimeStamp, opt => opt.MapFrom(src =>
                     FromDateTimeToTimeStamp(src.RegistrationDate)))
                 .ForMember(x => x.ProfilePhoto, opt => {
                     opt.PreCondition(src => src.ProfilePhoto != null);
                     opt.MapFrom(src =>
                     Google.Protobuf.ByteString.CopyFrom(src.ProfilePhoto));
                 })
                 .ForMember(x => x.Country, opt => opt.MapFrom((userDTO, user, country, context) =>
                 {
                     return userDTO.Country == null ? null : context.Mapper.Map<Country>(userDTO.Country);
                 }));


        }
        private Timestamp FromDateTimeToTimeStamp(DateTime? time)
        {
            return time == null ? null : Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)time, DateTimeKind.Utc));
        }
    }
}
