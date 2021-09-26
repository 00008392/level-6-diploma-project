using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Post.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post.API.Mappings
{
    public class PostGrpcMapper: Profile
    {
        public PostGrpcMapper()
        {
            CreateMap<BasePostRequest, CreatePostDTO>();

            CreateMap<BasePostRequest, UpdatePostDTO>();
               
            CreateMap<BasePostRequest, AccommodationManipulationDTO>()
                .Include<BasePostRequest, CreatePostDTO>()
                .Include<BasePostRequest, UpdatePostDTO>()
               .ForMember(x => x.OwnerId, opt => opt.MapFrom(src => src.OwnerId ?? 0))
               .ForMember(x => x.Price, opt => opt.MapFrom(src => (decimal)(src.Price ?? 0)))
               .ForMember(x => x.Latitude, opt => opt.MapFrom(src => (decimal?)src.Latitude))
               .ForMember(x => x.Longitude, opt => opt.MapFrom(src => (decimal?)src.Longitude))
               .ForMember(x => x.MovingInTime, opt => opt.MapFrom((src, dest) => src.MovingInTime?.ToDateTime()))
               .ForMember(x => x.MovingOutTime, opt => opt.MapFrom((src, dest) => src.MovingOutTime?.ToDateTime()));
            CreateMap<UpdatePostRequest, UpdatePostDTO>()
                .IncludeMembers(x => x.BaseRequest);
            CreateMap<OwnerDTO, Owner>();
            CreateMap<CategoryDTO, Category>();
            CreateMap<AccommodationInfoDTO, PostInfoResponse>()
                .ForMember(x => x.Owner, opt => opt.MapFrom((src, dest, prop, context) =>
                    {
                        return context.Mapper.Map<Owner>(src.Owner);
                    }))
                .ForMember(x => x.Category, opt => opt.MapFrom((src, dest, prop, context) =>
                {
                    return src.Category == null ? null : context.Mapper.Map<Category>(src.Category);
                }))
                .ForMember(x => x.Price, opt => opt.MapFrom(src => (double)src.Price))
                .ForMember(x => x.Latitude, opt => opt.MapFrom(src => (double?)src.Latitude))
                .ForMember(x => x.Longitude, opt => opt.MapFrom(src => (double?)src.Longitude))
                .ForMember(x => x.DatePublished, opt => opt.MapFrom(src =>
                    Timestamp.FromDateTime(DateTime.SpecifyKind(src.DatePublished, DateTimeKind.Utc))
                  ));
            CreateMap<ItemInfoDTO, Item>();
            CreateMap<AccommodationItemInfoDTO, AccommodationItem>()
                .ForMember(x => x.OtherValue, opt => opt.MapFrom(src => src.OtherItem))
                .ForMember(x => x.Base, opt => opt.MapFrom((src, dest, prop, context) =>
                    {
                        return context.Mapper.Map<Item>(src.Item);
                    }));
            CreateMap<AccommodationPhotoDTO, AccommodationPhoto>()
                .ForMember(x => x.Photo, opt => opt.MapFrom(src => src.Photo == null ? null : Google.Protobuf.ByteString.CopyFrom(src.Photo)));
            CreateMap<ItemRequest, AccommodationItemDTO>()
                .ForMember(x => x.OtherItem, opt => opt.MapFrom(src => src.OtherValue));
        }
    }
}
