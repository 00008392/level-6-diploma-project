using Account.Domain.Entities;
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
        //class for mapping domain entities to integration events and vice versa
        public AccountEventMapper()
        {
            //user
            CreateMap<User, UserCreatedOrUpdatedIntegrationEvent>()
                .ConvertUsing(x => new UserCreatedOrUpdatedIntegrationEvent(x.Id, x.FirstName, x.LastName, x.Email, x.PhoneNumber,
                x.DateOfBirth, (int)x.Gender, x.UserInfo, x.CountryId));
            //booking
            CreateMap<BookingAcceptedIntegrationEvent, Booking>()
                .ConvertUsing(x => new Booking(x.BookingId, x.GuestId, x.OwnerId, x.StartDate, x.EndDate));
        }
    }
}
