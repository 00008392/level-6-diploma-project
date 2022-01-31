using AutoMapper;
using Booking.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.API.Mappings
{
    public class BookingDomainMapper: Profile
    {
        public BookingDomainMapper()
        {
            //accommodation
            CreateMap<AccommodationDTO, Domain.Entities.Accommodation>()
                .ConvertUsing((x, context) =>
                {
                    return x.Id != 0 ? new Domain.Entities.Accommodation(x.Id, x.Title, x.OwnerId,
                x.Address, x.ContactNumber, x.RoomsNo, x.BathroomsNo, x.BedsNo,
                x.MaxGuestsNo, x.SquareMeters, x.Price, x.IsWholeApartment,
                x.MovingInTime, x.MovingOutTime) : new Domain.Entities.Accommodation(x.Title, x.OwnerId,
                x.Address, x.ContactNumber, x.RoomsNo, x.BathroomsNo, x.BedsNo,
                x.MaxGuestsNo, x.SquareMeters, x.Price, x.IsWholeApartment,
                x.MovingInTime, x.MovingOutTime);
                });

            CreateMap<Domain.Entities.Accommodation, AccommodationDTO>()
              .ConvertUsing(x => new AccommodationDTO(x.Id, x.Title, x.OwnerId, x.Address,
              x.ContactNumber, x.RoomsNo, x.BathroomsNo, x.BedsNo, x.MaxGuestsNo,
              x.SquareMeters, x.Price, x.IsWholeApartment, x.MovingInTime, x.MovingOutTime));

            //user
            CreateMap<Domain.Entities.User, UserDTO>()
              .ConvertUsing(x => new UserDTO(x.Id, x.FirstName, x.LastName, x.Email,
              x.PhoneNumber, x.Address));

            CreateMap<UserDTO, Domain.Entities.User>()
                .ConvertUsing(x => new Domain.Entities.User(x.Id, x.FirstName,
                x.LastName, x.Email, x.PhoneNumber, x.Address));

            CreateMap<CreateUserDTO, Domain.Entities.User>()
                .ConvertUsing(x => new Domain.Entities.User(x.Email, x.FirstName, x.LastName));

            //booking request
            CreateMap<Domain.Entities.BookingRequest, BookingInfoDTO>()
                .ConvertUsing((x, dest, context) =>
                {
                   var coTravelers = context.Mapper.Map<ICollection<UserDTO>>(x.CoTravelers);
                    var guest = context.Mapper.Map<UserDTO>(x.Guest);
                    var accommodation = context.Mapper.Map<AccommodationDTO>(x.Accommodation);
                    return new BookingInfoDTO(x.Id, guest, accommodation, coTravelers, x.GuestNo,
                        x.StartDate, x.EndDate, x.Status);
                });
          
            CreateMap<CreateBookingRequestDTO, Domain.Entities.BookingRequest>()
                .ConvertUsing(x => new Domain.Entities.BookingRequest(x.GuestId,
                x.AccommodationId, x.GuestNo, (DateTime)x.StartDate, (DateTime)x.EndDate));

        }
    }
}
