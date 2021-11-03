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
                .ConvertUsing(x => new UserCreatedIntegrationEvent(x.Email,
                x.FirstName, x.LastName, (DateTime)x.DateOfBirth, (int)x.Gender, DateTime.Now));
            CreateMap<UserUpdateDTO, UserUpdatedIntegrationEvent>()
                .ConvertUsing(x => new UserUpdatedIntegrationEvent(x.Id, x.FirstName,
                x.LastName, x.Email, x.PhoneNumber, (DateTime)x.DateOfBirth,
                (int)x.Gender, x.Address,
                x.UserInfo, x.CityId));

        }
    }
}
