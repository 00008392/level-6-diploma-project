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
        //class that maps domain entities to dtos and vice versa
        public BookingDomainMapper()
        {
            //booking 
            CreateMap<CreateBookingDTO, Domain.Entities.Booking>()
            .ConvertUsing(x => new Domain.Entities.Booking(x.GuestId,
            x.PostId, x.GuestNo, (DateTime)x.StartDate, (DateTime)x.EndDate));
            CreateMap<Domain.Entities.Booking, BookingInfoDTO>()
                .ConvertUsing((x, dest, context) =>
                {
                    return new BookingInfoDTO(x.Id, x.GuestId,
                        context.Mapper.Map<PostDTO>(x.Post), x.GuestNo,
                        x.StartDate, x.EndDate, x.Status);
                });
            //post
            CreateMap<Domain.Entities.Post, PostDTO>()
                .ConvertUsing(x => new PostDTO(x.Id, x.OwnerId, x.MaxGuestsNo));
        }
    }
}
