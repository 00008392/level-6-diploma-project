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
                .ForMember(x => x.DateOfBirth, opt => opt.MapFrom((src, dest) => src.DateOfBirth?.ToDateTime()))
                .ForMember(x => x.Gender, opt => opt.MapFrom(src => (Gender?)src.Gender));
            CreateMap<CityDTO, City>();
            CreateMap<CountryDTO, Country>();
            CreateMap<ProfileInfoDTO, ProfileInfoResponse>()
                .ForMember(x => x.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth == null ? null :
                    Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)src.DateOfBirth, DateTimeKind.Utc))))
                .ForMember(x => x.RegistrationDate, opt => opt.MapFrom(src =>
                    Timestamp.FromDateTime(DateTime.SpecifyKind(src.RegistrationDate, DateTimeKind.Utc))))
                .ForMember(x => x.ProfilePhoto, opt => opt.MapFrom(src => src.ProfilePhoto == null ? null :
                    Google.Protobuf.ByteString.CopyFrom(src.ProfilePhoto)))
                .ForMember(x => x.City, opt => opt.MapFrom((userDTO, user, city, context) =>
                {
                    return userDTO.City == null ? null : context.Mapper.Map<City>(userDTO.City);
                }))
                .ForMember(x => x.Country, opt => opt.MapFrom((userDTO, user, country, context) =>
                {
                    return userDTO.Country == null ? null : context.Mapper.Map<Country>(userDTO.Country);
                }));

        }
    }
}
