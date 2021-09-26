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
            CreateMap<AccommodationDTO, Domain.Entities.Accommodation>();
            CreateMap<Domain.Entities.BookingRequest, BookingRequestInfoDTO>()
                .ConstructUsing(x => new BookingRequestInfoDTO(x.StartDate,
                x.EndDate, x.Status));
            CreateMap<UserDTO, Domain.Entities.User>();
            CreateMap<CreateBookingRequestDTO, Domain.Entities.BookingRequest>()
                .ForMember(x => x.StartDate, opt => opt.MapFrom(src => (DateTime)src.StartDate))
                .ForMember(x => x.EndDate, opt => opt.MapFrom(src => (DateTime)src.EndDate));

        }
    }
}
