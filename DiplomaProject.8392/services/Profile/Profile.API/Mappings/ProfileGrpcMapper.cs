using Google.Protobuf.WellKnownTypes;
using Profile.Domain.Enums;
using Profile.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Profile.API.Mappings
{
    public class ProfileGrpcMapper: AutoMapper.Profile
    {
        public ProfileGrpcMapper()
        {
            CreateMap<UpdateRequest, UpdateProfileDTO>()
                .ConvertUsing((x, context) => new UpdateProfileDTO(x.Id, x.FirstName, x.LastName,
                x.Email, x.PhoneNumber, x.DateOfBirth?.ToDateTime(), (Gender?)x.Gender,
                x.Address, x.UserInfo, x.CityId));
                     
       
            CreateMap<CityDTO, City>();
            CreateMap<CountryDTO, Country>();
            CreateMap<ProfileInfoDTO, ProfileInfoResponse>()
                .ForMember(x => x.DateOfBirth, opt => opt.MapFrom(src => FromDateTimeToTimeStamp(src.DateOfBirth)))
                .ForMember(x => x.RegistrationDate, opt => opt.MapFrom(src =>
                    FromDateTimeToTimeStamp(src.RegistrationDate)))
                .ForMember(x => x.ProfilePhoto, opt => {
                    opt.PreCondition(src => src.ProfilePhoto != null);
                    opt.MapFrom(src => 
                    Google.Protobuf.ByteString.CopyFrom(src.ProfilePhoto));
                } )
                .ForMember(x => x.City, opt => opt.MapFrom((userDTO, user, city, context) =>
                {
                    return userDTO.City == null ? null : context.Mapper.Map<City>(userDTO.City);
                }))
                .ForMember(x => x.Country, opt => opt.MapFrom((userDTO, user, country, context) =>
                {
                    return userDTO.Country == null ? null : context.Mapper.Map<Country>(userDTO.Country);
                }));

        }
        private Timestamp FromDateTimeToTimeStamp(DateTime? time)
        {
            return time == null? null: Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)time, DateTimeKind.Utc));
        }
    }
}
