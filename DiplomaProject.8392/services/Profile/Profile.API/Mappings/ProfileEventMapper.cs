using Profile.Domain.Logic.DTOs;
using Profile.Domain.Logic.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Profile.API.Mappings
{
    public class ProfileEventMapper: AutoMapper.Profile
    {
        public ProfileEventMapper()
        {
            CreateMap<UserCreatedIntegrationEvent, CreateProfileDTO>();
            CreateMap<UpdateProfileDTO, UserUpdatedIntegrationEvent>()
                .ForMember(x => x.UserId, opt => opt.MapFrom(src => src.Id));

        }
    }
}
