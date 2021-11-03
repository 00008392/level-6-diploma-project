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
            CreateMap<UserCreatedIntegrationEvent, CreateProfileDTO>()
                .ConvertUsing(x => new CreateProfileDTO(x.Email, x.RegistrationDate));
            CreateMap<UpdateProfileDTO, UserUpdatedIntegrationEvent>()
                .ConvertUsing(x => new UserUpdatedIntegrationEvent(x.Id,
                x.FirstName, x.LastName, x.Email, x.PhoneNumber, x.DateOfBirth,
                x.Gender, x.Address, x.UserInfo, x.CityId));

        }
    }
}
