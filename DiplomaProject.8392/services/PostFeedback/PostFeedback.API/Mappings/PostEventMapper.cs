using AutoMapper;
using PostFeedback.API.Mappings.Helpers;
using PostFeedback.Domain.Entities;
using PostFeedback.Domain.Logic.DTOs;
using PostFeedback.Domain.Logic.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostFeedback.API.Mappings
{
    public class PostEventMapper: Profile
    {
        //class for mapping dtos to integration events and events to domain entities
        public PostEventMapper()
        {
            //post
            CreateMap<Post, PostCreatedOrUpdatedIntegrationEvent>()
                .ConvertUsing(x => new PostCreatedOrUpdatedIntegrationEvent(x.Id, x.Title,
                x.Description, x.OwnerId, x.CategoryId, x.CityId, x.Address,
                x.ContactNumber, x.RoomsNo, x.BathroomsNo, x.BedsNo, x.MaxGuestsNo, x.SquareMeters,
                x.Price, x.IsWholeApartment, x.MovingInTime,
               x.MovingOutTime
               ));
            //user
            CreateMap<UserCreatedOrUpdatedIntegrationEvent, Domain.Entities.User>()
                .ConvertUsing(x => new Domain.Entities.User(x.UserId, x.FirstName,
                x.LastName, x.Email));
            //booking
            CreateMap<BookingAcceptedIntegrationEvent, Booking>()
                .ConvertUsing(x => new Booking(x.BookingId, x.PostId, x.GuestId,
                x.StartDate, x.EndDate));
        }
    }
}
