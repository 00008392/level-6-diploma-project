using AutoMapper;
using Post.Domain.Logic.DTOs;
using Post.Domain.Logic.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post.API.Mappings
{
    public class PostEventMapper: Profile
    {
        public PostEventMapper()
        {
            CreateMap<CreatePostDTO, AccommodationCreatedIntegrationEvent>()
                .ConvertUsing(x => new AccommodationCreatedIntegrationEvent(x.Title,
                x.Description, x.OwnerId, x.CategoryId, x.CityId, x.Address, x.ReferencePoint,
                x.ContactNumber, x.RoomsNo, x.BathroomsNo, x.BedsNo, x.MaxGuestsNo, x.SquareMeters,
                x.Price, x.Latitude, x.Longitude, x.IsWholeApartment,DateTimeToString(x.MovingInTime),
                DateTimeToString(x.MovingOutTime),
                x.AdditionalInfo, DateTime.Now));

            CreateMap<UpdatePostDTO, AccommodationUpdatedIntegrationEvent>()
                .ConvertUsing(x => new AccommodationUpdatedIntegrationEvent(x.Id, x.Title,
                x.Description, x.OwnerId, x.CityId, x.CategoryId, x.Address, x.ReferencePoint,
                x.ContactNumber, x.RoomsNo, x.BathroomsNo, x.BedsNo, x.MaxGuestsNo, x.SquareMeters,
                x.Price, x.Latitude, x.Longitude, x.IsWholeApartment, DateTimeToString(x.MovingInTime),
                DateTimeToString(x.MovingOutTime), x.AdditionalInfo
               ));
            CreateMap<UserUpdatedIntegrationEvent, UpdateUserDTO>()
                .ConvertUsing(x => new UpdateUserDTO(x.UserId, x.FirstName,
                x.LastName, x.PhoneNumber, x.Email));
            CreateMap<UserCreatedIntegrationEvent, CreateUserDTO>()
                .ConvertUsing(x => new CreateUserDTO(x.Email, x.FirstName, x.LastName));
            CreateMap<AccommodationBookedIntegrationEvent, AddBookingDTO>()
                .ConvertUsing(x => new AddBookingDTO(x.BookingId, x.AccommodationId, x.GuestId,
                x.StartDate, x.EndDate));
        }
        private string DateTimeToString(DateTime? time)
        {
            return time?.ToString("HH:mm");
        }
    }
}
