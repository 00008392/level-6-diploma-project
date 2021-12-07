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
            CreateMap<AccommodationCreatedIntegrationEvent, AccommodationDTO>()
                .ConvertUsing(x => new AccommodationDTO(x.Title, x.OwnerId, x.Address,
                x.ContactNumber, x.RoomsNo, x.BathroomsNo, x.BedsNo, x.MaxGuestsNo,
                x.SquareMeters, x.Price, x.IsWholeApartment, x.MovingInTime,
                x.MovingOutTime));

            CreateMap<AccommodationUpdatedIntegrationEvent, AccommodationDTO>()
                .ConvertUsing(x => new AccommodationDTO(x.AccommodationId, x.Title, x.OwnerId, x.Address,
                x.ContactNumber, x.RoomsNo, x.BathroomsNo, x.BedsNo, x.MaxGuestsNo,
                x.SquareMeters, x.Price, x.IsWholeApartment, x.MovingInTime,
                x.MovingOutTime));

            //user
            CreateMap<UserCreatedIntegrationEvent, CreateUserDTO>()
                .ConvertUsing(x => new CreateUserDTO(x.Email, x.FirstName, x.LastName));

            CreateMap<UserUpdatedIntegrationEvent, UserDTO>()
                .ConvertUsing(x => new UserDTO(x.UserId, x.FirstName, x.LastName,
                x.Email, x.PhoneNumber, x.Address, x.DateOfBirth));

            // booking
            CreateMap<BookingRequestInfoDTO, AccommodationBookedIntegrationEvent>()
                .ConvertUsing(x => new AccommodationBookedIntegrationEvent(x.Id, x.Accommodation.Id,
                x.Guest.Id, x.StartDate, x.EndDate));

            CreateMap<BookingRequestInfoDTO, AccommodationBookingCancelledIntegrationEvent>()
              .ConvertUsing(x => new AccommodationBookingCancelledIntegrationEvent(x.Id));
        }
    }
}
