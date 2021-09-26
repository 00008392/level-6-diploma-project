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
                .ForMember(x => x.MovingInTime, opt => opt.MapFrom((src, dest) => src.MovingInTime?.ToString("HH:mm")))
                .ForMember(x => x.MovingOutTime, opt => opt.MapFrom((src, dest) => src.MovingOutTime?.ToString("HH:mm")))
                .ForMember(x => x.DatePublished, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<UpdatePostDTO, AccommodationUpdatedIntegrationEvent>()
                .ForMember(x => x.MovingInTime, opt => opt.MapFrom((src, dest) => src.MovingInTime?.ToString("HH:mm")))
                .ForMember(x => x.MovingOutTime, opt => opt.MapFrom((src, dest) => src.MovingOutTime?.ToString("HH:mm")))
                .ForMember(x => x.AccommodationId, opt => opt.MapFrom(src => src.Id));
            CreateMap<UserUpdatedIntegrationEvent, UpdateUserDTO>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.UserId));
        }
    }
}
