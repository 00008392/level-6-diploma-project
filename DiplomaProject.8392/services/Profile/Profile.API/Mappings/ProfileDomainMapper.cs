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
                .ConstructUsing(x => new User(x.Email, x.RegistrationDate));
            CreateMap<Domain.Entities.City, CityDTO>();
            CreateMap<Domain.Entities.Country, CountryDTO>();
            CreateMap<User, ProfileInfoDTO>()
                .ForMember(x => x.City, opt => opt.MapFrom((user, userDTO, cityDTO, context) =>
                    {
                        return user.City==null?null: context.Mapper.Map<CityDTO>(user.City);
                    }
                  ))
                .ForMember(x => x.Country, opt => opt.MapFrom((user, userDTO, countryDTO, context) =>
                    {
                        return user.City==null?null: context.Mapper.Map<CountryDTO>(user.City.Country);
                    }));
            CreateMap<UpdateProfileDTO, User>();
                
        }
    }
}
