using Account.API;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGateway.Mappings
{
    public class AccountMapper: Profile
    {
        public AccountMapper()
        {
            CreateMap<UserInfoResponse, Models.Account.UserInfoResponse>()
                .ForMember(x => x.RegistrationDate, opt => opt.MapFrom((src, dest) => src.RegistrationDate?.ToDateTime()))
                .ForMember(x => x.DateOfBirth, opt => opt.MapFrom((src, dest) => src.DateOfBirth?.ToDateTime()));
            CreateMap<Models.Account.RegistrationRequest, RegistrationRequest>()
                 .ForMember(x => x.DateOfBirth, opt => opt.MapFrom(src => FromDateTimeToTimeStamp(src.DateOfBirth)));
            CreateMap<Models.Account.UpdateRequest, UpdateRequest>()
                 .ForMember(x => x.DateOfBirth, opt => opt.MapFrom(src => FromDateTimeToTimeStamp(src.DateOfBirth)));

        }

        private Timestamp FromDateTimeToTimeStamp(DateTime? time)
        {
            return time == null ? null : Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)time, DateTimeKind.Utc));
        }
    }
}
