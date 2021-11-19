using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Post.Domain.Logic.DTOs;
using Protos.Common;
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
            CreateMap<CreatePostRequest, CreatePostDTO>()
                 .ConvertUsing((x, context) => new CreatePostDTO(x.Title, x.Description,
                x.OwnerId ?? 0, x.CategoryId, x.Address, x.ReferencePoint, x.ContactNumber,
                x.RoomsNo, x.BathroomsNo, x.BedsNo, x.MaxGuestsNo ?? 0, x.SquareMeters,
                (decimal)(x.Price ?? 0), (decimal?)x.Latitude, (decimal?)x.Longitude, x.IsWholeApartment, x.AdditionalInfo,
                x.MovingInTimeStamp?.ToDateTime(), x.MovingOutTimeStamp?.ToDateTime()));

            CreateMap<UpdatePostRequest, UpdatePostDTO>()
                .ConvertUsing((x, context) =>
                {
                   
                    return new UpdatePostDTO(x.Id, x.Title, x.Description,
                    x.OwnerId ?? 0, x.CategoryId, x.Address, x.ReferencePoint, x.ContactNumber,
                    x.RoomsNo, x.BathroomsNo, x.BedsNo, x.MaxGuestsNo ?? 0, x.SquareMeters,
                    (decimal)(x.Price ?? 0), (decimal?)x.Latitude, (decimal?)x.Longitude, x.IsWholeApartment,
                    x.AdditionalInfo,
                    x.MovingInTimeStamp?.ToDateTime(), x.MovingOutTimeStamp?.ToDateTime());
                });
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
                .ForMember(x => x.DatePublishedTimeStamp, opt => opt.MapFrom(src =>
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
                .ConvertUsing(x => new AccommodationItemDTO(x.AccommodationId,
                x.ItemId, x.OtherValue));

        }
    }
}
