using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Helpers;
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
        //class for mapping grpc generated classes to dtos and vice versa
        public PostGrpcMapper()
        {
            //post
            CreateMap<CreatePostRequest, PostManipulationDTO>()
                 .ConvertUsing((x, context) => new PostManipulationDTO(x.Title, x.Description,
                x.OwnerId ?? 0, x.CategoryId, x.CityId ?? 0, x.Address, x.ContactNumber,
                x.RoomsNo, x.BathroomsNo, x.BedsNo ?? 0, x.MaxGuestsNo ?? 0, x.SquareMeters,
                (decimal)(x.Price ?? 0), x.IsWholeApartment ?? false,
                x.MovingInTimeStamp?.ToDateTime(), x.MovingOutTimeStamp?.ToDateTime(),
                x.Rules, x.Facilities));
            CreateMap<UpdatePostRequest, PostManipulationDTO>()
                .ConvertUsing((x, context) =>
                {
                    return new PostManipulationDTO(x.Id, x.Title, x.Description,
                    x.OwnerId ?? 0, x.CategoryId, x.CityId ?? 0, x.Address, x.ContactNumber,
                    x.RoomsNo, x.BathroomsNo, x.BedsNo ?? 0, x.MaxGuestsNo ?? 0, x.SquareMeters,
                    (decimal)(x.Price ?? 0), x.IsWholeApartment ?? false,
                    x.MovingInTimeStamp?.ToDateTime(), x.MovingOutTimeStamp?.ToDateTime(),
                    x.Rules, x.Facilities);
                });
            CreateMap<PostDetailsDTO, PostResponse>()
                .ForMember(x => x.Owner, opt => opt.MapFrom((src, dest, prop, context) =>
                {
                    return context.Mapper.Map<User>(src.Owner);
                }))
                .ForMember(x => x.Price, opt => opt.MapFrom(src => (double)src.Price))
                .ForMember(x => x.DatePublishedTimeStamp, opt => opt.MapFrom(src =>
                    GrpcServiceHelper.ConvertDateTimeToTimeStamp(src.DatePublished)
                  ));
            //post related info
            CreateMap<UserDTO, User>();
            CreateMap<ItemDTO, Item>();
            CreateMap<PhotoDTO, Photo>()
                .ForMember(x => x.Photo_, opt => opt.MapFrom(src => src.Photo == null ? null : Google.Protobuf.ByteString.CopyFrom(src.Photo)));
            //filter
            CreateMap<FilterRequest, FilterParameters>();
            //feedback
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
                .ConvertUsing(x => new FeedbackDTO(x.CreatorId, x.ItemId ?? 0, x.Rating ?? 0, x.Message));

        }
    }
}
