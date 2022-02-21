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
        //class that maps dtos to events and events to domain entities
        public BookingEventMapper()
        {
            //post
            CreateMap<PostCreatedOrUpdatedIntegrationEvent, Domain.Entities.Post>()
                .ConvertUsing(x => new Domain.Entities.Post(x.PostId, x.OwnerId, x.MaxGuestsNo));

            // booking
            CreateMap<Domain.Entities.Booking, BookingAcceptedIntegrationEvent>()
                .ConvertUsing((x, dest) => new BookingAcceptedIntegrationEvent(x.Id,
                x.PostId, x.GuestId, x.Post?.OwnerId??0, x.StartDate, x.EndDate));
        }
    }
}
