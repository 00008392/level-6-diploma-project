using AutoMapper;
using Booking.Domain.Logic.DTOs;
using Grpc.Base.Helpers;
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
            //Post
            CreateMap<PostDTO, Post>();
            //Booking
            CreateMap<BookingInfoDTO, BookingInfoResponse>()
                .ForMember(x => x.Post, opt => opt.MapFrom((src, dest, prop, context) =>
                {
                    return context.Mapper.Map<Post>(src.Post);
                }))
                .ForMember(x => x.StartDateTimeStamp, opt => opt.MapFrom(src =>
                    GrpcServiceHelper.ConvertDateTimeToTimeStamp(src.StartDate)))
                .ForMember(x => x.EndDateTimeStamp, opt => opt.MapFrom(src =>
                    GrpcServiceHelper.ConvertDateTimeToTimeStamp(src.EndDate)))
                .ForMember(x => x.Status, opt => opt.MapFrom(src => (int)src.Status));
            CreateMap<CreateRequest, CreateBookingDTO>()
                .ConvertUsing((x, context) => new CreateBookingDTO(x.GuestId??0,
                x.PostId??0, x.GuestNo??0, x.StartDateTimeStamp?.ToDateTime(),
                x.EndDateTimeStamp?.ToDateTime()));

        }
    }
}
