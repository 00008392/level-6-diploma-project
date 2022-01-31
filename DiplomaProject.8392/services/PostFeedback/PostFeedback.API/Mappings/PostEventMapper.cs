using AutoMapper;
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
        public PostEventMapper()
        {
            CreateMap<PostManipulationDTO, PostCreatedIntegrationEvent>()
                .ConvertUsing(x => new PostCreatedIntegrationEvent(x.Title,
                x.Description, x.OwnerId, x.CategoryId, x.CityId, x.Address, x.ReferencePoint,
                x.ContactNumber, x.RoomsNo, x.BathroomsNo, x.BedsNo, x.MaxGuestsNo, x.SquareMeters,
                x.Price, x.IsWholeApartment,DateTimeToString(x.MovingInTime),
                DateTimeToString(x.MovingOutTime),
                x.AdditionalInfo, DateTime.Now));

            CreateMap<PostManipulationDTO, PostUpdatedIntegrationEvent>()
                .ConvertUsing(x => new PostUpdatedIntegrationEvent(x.Id, x.Title,
                x.Description, x.OwnerId, x.CityId, x.CategoryId, x.Address, x.ReferencePoint,
                x.ContactNumber, x.RoomsNo, x.BathroomsNo, x.BedsNo, x.MaxGuestsNo, x.SquareMeters,
                x.Price, x.IsWholeApartment, DateTimeToString(x.MovingInTime),
                DateTimeToString(x.MovingOutTime), x.AdditionalInfo
               ));
            CreateMap<UserUpdatedIntegrationEvent, UserDTO>()
                .ConvertUsing(x => new UserDTO(x.UserId, x.FirstName,
                x.LastName, x.PhoneNumber, x.Email));
            CreateMap<UserCreatedIntegrationEvent, UserDTO>()
                .ConvertUsing(x => new UserDTO(x.Email, x.FirstName, x.LastName));
            CreateMap<BookingAcceptedIntegrationEvent, AddBookingDTO>()
                .ConvertUsing(x => new AddBookingDTO(x.BookingId, x.PostId, x.GuestId,
                x.StartDate, x.EndDate));
        }
        private string DateTimeToString(DateTime? time)
        {
            return time?.ToString("HH:mm");
        }
    }
}
