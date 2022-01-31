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
            //accommodation
            CreateMap<PostCreatedIntegrationEvent, AccommodationDTO>()
                .ConvertUsing(x => new AccommodationDTO(x.Title, x.OwnerId, x.Address,
                x.ContactNumber, x.RoomsNo, x.BathroomsNo, x.BedsNo, x.MaxGuestsNo,
                x.SquareMeters, x.Price, x.IsWholeApartment, x.MovingInTime,
                x.MovingOutTime));

            CreateMap<PostUpdatedIntegrationEvent, AccommodationDTO>()
                .ConvertUsing(x => new AccommodationDTO(x.PostId, x.Title, x.OwnerId, x.Address,
                x.ContactNumber, x.RoomsNo, x.BathroomsNo, x.BedsNo, x.MaxGuestsNo,
                x.SquareMeters, x.Price, x.IsWholeApartment, x.MovingInTime,
                x.MovingOutTime));

            //user
            CreateMap<UserCreatedIntegrationEvent, CreateUserDTO>()
                .ConvertUsing(x => new CreateUserDTO(x.Email, x.FirstName, x.LastName));

            CreateMap<UserUpdatedIntegrationEvent, UserDTO>()
                .ConvertUsing(x => new UserDTO(x.UserId, x.FirstName, x.LastName,
                x.Email, x.PhoneNumber, x.Address));

            // booking
            CreateMap<BookingInfoDTO, BookingAcceptedIntegrationEvent>()
                .ConvertUsing(x => new BookingAcceptedIntegrationEvent(x.Id, x.Accommodation.Id,
                x.Guest.Id, x.Accommodation.OwnerId, x.StartDate, x.EndDate));

            CreateMap<BookingInfoDTO, BookingCancelledIntegrationEvent>()
              .ConvertUsing(x => new BookingCancelledIntegrationEvent(x.Id));
        }
    }
}
