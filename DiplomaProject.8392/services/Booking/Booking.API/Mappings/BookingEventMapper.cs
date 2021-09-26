using AutoMapper;
using Booking.Domain.Logic.DTOs;
using Booking.Domain.Logic.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.API.Mappings
{
    public class BookingEventMapper: Profile
    {
        public BookingEventMapper()
        {
            CreateMap<AccommodationCreatedIntegrationEvent, AccommodationDTO>();
            CreateMap<UserUpdatedIntegrationEvent, UserDTO>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.UserId));
        }
    }
}
