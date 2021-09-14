using Account.Domain.Logic.DTOs;
using Account.Domain.Logic.IntegrationEvents.Events;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.API.Mappings
{
    public class AccountEventMapper: Profile
    {
        public AccountEventMapper()
        {
            CreateMap<UserRegistrationDTO, UserCreatedIntegrationEvent>()
                .ForMember(u => u.RegistrationDate, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<UserUpdatedIntegrationEvent, UpdateUserDTO>()
                .ForMember(u => u.Id, opt => opt.MapFrom(src => src.UserId));
        }
    }
}
