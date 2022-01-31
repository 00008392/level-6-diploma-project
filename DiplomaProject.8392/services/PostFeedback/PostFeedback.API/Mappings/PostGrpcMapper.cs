using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using PostFeedback.Domain.Logic.DTOs;
using PostFeedback.Domain.Logic.Filter;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostFeedback.API.Mappings
{
    public class PostGrpcMapper: Profile
    {
        public PostGrpcMapper()
        {
            CreateMap<CreatePostRequest, PostManipulationDTO>()
                 .ConvertUsing((x, context) => new PostManipulationDTO(x.Title, x.Description,
                x.OwnerId ?? 0, x.CategoryId, x.CityId, x.Address, x.ReferencePoint, x.ContactNumber,
                x.RoomsNo, x.BathroomsNo, x.BedsNo, x.MaxGuestsNo ?? 0, x.SquareMeters,
                (decimal)(x.Price ?? 0), x.IsWholeApartment, x.AdditionalInfo,
                x.MovingInTimeStamp?.ToDateTime(), x.MovingOutTimeStamp?.ToDateTime()));

            CreateMap<UpdatePostRequest, PostManipulationDTO>()
                .ConvertUsing((x, context) =>
                {

                    return new PostManipulationDTO(x.Id, x.Title, x.Description,
                    x.OwnerId ?? 0, x.CategoryId, x.CityId, x.Address, x.ReferencePoint, x.ContactNumber,
                    x.RoomsNo, x.BathroomsNo, x.BedsNo, x.MaxGuestsNo ?? 0, x.SquareMeters,
                    (decimal)(x.Price ?? 0), x.IsWholeApartment,
                    x.AdditionalInfo,
                    x.MovingInTimeStamp?.ToDateTime(), x.MovingOutTimeStamp?.ToDateTime());
                });
            CreateMap<UserDTO, User>();
            CreateMap<CategoryCityDTO, CategoryCity>();
            CreateMap<PostDetailsDTO, PostResponse>()
                .ForMember(x => x.Owner, opt => opt.MapFrom((src, dest, prop, context) =>
                    {
                        return context.Mapper.Map<User>(src.Owner);
                    }))
                .ForMember(x => x.City, opt => opt.MapFrom((src, dest, prop, context) =>
                {
                    return src.City == null ? null : context.Mapper.Map<CategoryCity>(src.City);
                }))
                .ForMember(x => x.Category, opt => opt.MapFrom((src, dest, prop, context) =>
                {
                    return src.Category == null ? null : context.Mapper.Map<CategoryCity>(src.Category);
                }))
                .ForMember(x => x.Price, opt => opt.MapFrom(src => (double)src.Price))
                .ForMember(x => x.DatePublishedTimeStamp, opt => opt.MapFrom(src =>
                    Timestamp.FromDateTime(DateTime.SpecifyKind(src.DatePublished, DateTimeKind.Utc))
                  ));
            CreateMap<FilterRequest, FilterParameters>();
            CreateMap<ItemInfoDTO, Item>();
            CreateMap<PostItemInfoDTO, PostItem>();
            CreateMap<PhotoDTO, Photo>()
                .ForMember(x => x.Photo_, opt => opt.MapFrom(src => src.Photo == null ? null : Google.Protobuf.ByteString.CopyFrom(src.Photo)));
            CreateMap<ItemRequest, AddItemToPostDTO>()
                .ConvertUsing(x => new AddItemToPostDTO(
                x.ItemId, x.OtherValue));

            CreateMap<FeedbackInfoDTO<UserDTO>, FeedbackResponse>()
                .ForMember(x => x.User, opt => opt.MapFrom((src, dest, prop, context) =>
                    {
                        return context.Mapper.Map<User>(src.Item);
                    }))

                .ForMember(x => x.Accommodation, opt => opt.Ignore());
            CreateMap<FeedbackInfoDTO<PostDetailsDTO>, FeedbackResponse>()
              .ForMember(x => x.Accommodation, opt => opt.MapFrom((src, dest, prop, context) =>
              {
                  return context.Mapper.Map<PostResponse>(src.Item);
              }))
              .ForMember(x => x.User, opt => opt.Ignore());
            CreateMap<CreateFeedbackRequest, FeedbackDTO>()
                .ConvertUsing(x => new FeedbackDTO(x.UserId, x.ItemId ?? 0, x.Rating ?? 0, x.Message));

        }
    }
}
