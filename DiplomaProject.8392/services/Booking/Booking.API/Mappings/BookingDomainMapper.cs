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
            CreateMap<AccommodationDTO, Domain.Entities.Accommodation>()
                .ConvertUsing((x, context) =>
                {
                    return x.Id !=0 ? new Domain.Entities.Accommodation(x.Id, x.Title, x.OwnerId,
                x.Address, x.ContactNumber, x.RoomsNo, x.BathroomsNo, x.BedsNo,
                x.MaxGuestsNo, x.SquareMeters, x.Price, x.IsWholeApartment,
                x.MovingInTime, x.MovingOutTime) : new Domain.Entities.Accommodation(x.Title, x.OwnerId,
                x.Address, x.ContactNumber, x.RoomsNo, x.BathroomsNo, x.BedsNo,
                x.MaxGuestsNo, x.SquareMeters, x.Price, x.IsWholeApartment,
                x.MovingInTime, x.MovingOutTime);
                });

            //CreateMap<Domain.Entities.BookingRequest, BookingRequestInfoDTO>()
            //    .ConvertUsing((x, dest, context) => {
            //        List<UserDTO> coTravelers = new List<UserDTO>();
            //        x.CoTravelers.ToList().ForEach(i => 
            //        coTravelers.Add(context.Mapper.Map<UserDTO>(i.CoTraveler)));
            //        return new BookingRequestInfoDTO(x.Id, coTravelers, x.GuestNo,
            //            x.StartDate, x.EndDate, x.Status);
            //    });
            CreateMap<CoTravelerDTO, Domain.Entities.CoTravelerBooking>()
                .ConvertUsing(x => new Domain.Entities.CoTravelerBooking(x.BookingId, x.CoTravelerId));
            CreateMap<Domain.Entities.Accommodation, AccommodationDTO>()
                .ConvertUsing(x => new AccommodationDTO(x.Id, x.Title, x.OwnerId, x.Address,
                x.ContactNumber, x.RoomsNo, x.BathroomsNo, x.BedsNo, x.MaxGuestsNo,
                x.SquareMeters, x.Price, x.IsWholeApartment, x.MovingInTime, x.MovingOutTime));
            CreateMap<Domain.Entities.User, UserDTO>()
                .ConvertUsing(x => new UserDTO(x.Id, x.FirstName, x.LastName, x.Email,
                x.PhoneNumber, x.Address, x.DateOfBirth));
            CreateMap<UserDTO, Domain.Entities.User>()
                .ConvertUsing(x => new Domain.Entities.User(x.Id, x.FirstName,
                x.LastName,x.Email, x.PhoneNumber, x.Address,x.DateOfBirth));
            CreateMap<CreateBookingRequestDTO, Domain.Entities.BookingRequest>()
                .ConvertUsing(x => new Domain.Entities.BookingRequest(x.GuestId,
                x.AccommodationId, x.GuestNo, (DateTime)x.StartDate, (DateTime)x.EndDate));

        }
    }
}
