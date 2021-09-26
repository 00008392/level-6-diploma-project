using AutoMapper;
using Booking.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.API.Mappings
{
    public class BookingGrpcMapper: Profile
    {
        public BookingGrpcMapper()
        {
            CreateMap<UserDTO, User>()
                .ForMember(x => x.DateOfBirth, opt => opt.MapFrom(
                      src => src.DateOfBirth == null ? null : Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime
                      (DateTime.SpecifyKind((DateTime)src.DateOfBirth, DateTimeKind.Utc))
                      ));
            CreateMap<AccommodationDTO, Accommodation>()
                .ForMember(x => x.Price, opt => opt.MapFrom(src => (double)src.Price));
            CreateMap<BookingRequestInfoDTO, BookingRequest>()
                .ForMember(x => x.Guest, opt => opt.MapFrom((src, dest, prop, context) =>
                    {
                        return src.Guest == null ? null : context.Mapper.Map<User>(src.Guest);
                    }))
                .ForMember(x => x.Accommodation, opt => opt.MapFrom((src, dest, prop, context) =>
                {
                    return src.Accommodation == null ? null : context.Mapper.Map<Accommodation>(src.Accommodation);
                }))
                .ForMember(x => x.StartDate, opt => opt.MapFrom
                  (src => Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime
                  (DateTime.SpecifyKind(src.StartDate, DateTimeKind.Utc))))
                .ForMember(x => x.EndDate, opt => opt.MapFrom
                (src => Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime
                (DateTime.SpecifyKind(src.EndDate, DateTimeKind.Utc))))
                .ForMember(x => x.Status, opt => opt.MapFrom(src => (int)src.Status));
            CreateMap<CreateRequest, CreateBookingRequestDTO>()
                .ForMember(x => x.GuestId, opt => opt.MapFrom(src => src.GuestId ?? 0))
                .ForMember(x => x.AccommodationId, opt => opt.MapFrom(src => src.AccommodationId ?? 0))
                .ForMember(x => x.StartDate, opt => opt.MapFrom(
                      (src, dest) => src.StartDate?.ToDateTime()
                      ))
                .ForMember(x => x.EndDate, opt => opt.MapFrom(
                      (src, dest) => src.EndDate?.ToDateTime()
                      ));
        }
    }
}
