using AutoMapper;
using PostFeedback.Domain.Entities;
using PostFeedback.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PostFeedback.API.Mappings
{
    public class PostDomainMapper: Profile
    {
        public PostDomainMapper()
        {
        //    CreateMap<PostManipulationDTO, Post>()
        //       .ConvertUsing(x => new Post(x.Title, x.Description, x.OwnerId,
        //        DateTime.Now, x.CategoryId, x.CityId, x.Address, x.ReferencePoint, x.ContactNumber,
        //        x.RoomsNo, x.BathroomsNo, x.BedsNo, x.MaxGuestsNo, x.SquareMeters,
        //        x.Price, x.IsWholeApartment, DateTimeToString(x.MovingInTime),
        //       DateTimeToString(x.MovingOutTime), x.AdditionalInfo));
        //    CreateMap<Domain.Entities.User, UserDTO>()
        //        .ConvertUsing(x => new UserDTO(x.Id, x.FirstName, x.LastName,
        //        x.Email, x.PhoneNumber));
        //    CreateMap<Category, CategoryCityDTO>()
        //        .ConvertUsing(x => new CategoryCityDTO(x.Id, x.Name));
        //    CreateMap<City, CategoryCityDTO>()
        //        .ConvertUsing(x => new CategoryCityDTO(x.Id, x.Name));
        //    CreateMap<Domain.Entities.Photo, PhotoDTO>()
        //        .ConvertUsing(x => new PhotoDTO(x.Id, x.Photo, x.MimeType));
        //    CreateMap<Domain.Entities.Item, ItemInfoDTO>()
        //        .ConvertUsing(x => new ItemInfoDTO(x.Id, x.Name, x.IsOther));
        //    CreateMap<Domain.Entities.PostItem, PostItemInfoDTO>()
        //        .ConvertUsing(x => new PostItemInfoDTO(x.Id,
        //        x.OtherValue, x.Item.Name)
        //        );
        //    CreateMap<PostFacility, PostItemInfoDTO>()
        //          .ConvertUsing(x => new PostItemInfoDTO(x.Item.Id,
        //        x.OtherValue, x.Item.Name)
        //        );
        //    CreateMap<PostRule, PostItemInfoDTO>()
        //          .ConvertUsing(x => new PostItemInfoDTO(x.Item.Id,
        //        x.OtherValue, x.Item.Name)
        //        );
        //    CreateMap<PostSpecificity, PostItemInfoDTO>()
        //          .ConvertUsing(x => new PostItemInfoDTO(x.Item.Id,
        //        x.OtherValue, x.Item.Name)
        //        );
        //    CreateMap<Post, PostDetailsDTO>()
        //        .ConvertUsing((x, dest, context) => new PostDetailsDTO(x.Id,
        //        x.Title, x.Description, x.Address,
        //        x.ReferencePoint, x.ContactNumber, x.RoomsNo, x.BathroomsNo,
        //        x.BedsNo, x.MaxGuestsNo, x.SquareMeters, x.Price,
        //        x.IsWholeApartment, x.AdditionalInfo, x.Owner == null ? null : context.Mapper.Map<UserDTO>(x.Owner),
        //        x.DatePublished, x.Category == null ? null : context.Mapper.Map<CategoryCityDTO>(x.Category),
        //        x.City == null ? null : context.Mapper.Map<CategoryCityDTO>(x.City),
        //        x.MovingInTime, x.MovingOutTime,
        //        MapCollection<Domain.Entities.Photo, PhotoDTO>(x.Photos, context),
        //        MapCollection<PostSpecificity, PostItemInfoDTO>(x.Specificities, context),
        //        MapCollection<PostRule, PostItemInfoDTO>(x.Rules, context),
        //        MapCollection<PostFacility, PostItemInfoDTO>(x.Facilities, context)
        //        ));

        //    CreateMap<UserDTO, Domain.Entities.User>()
        //        .ConvertUsing(x => x.Id == 0 ? new Domain.Entities.User(x.Email, x.FirstName, x.LastName) :
        //        new Domain.Entities.User(x.Id, x.FirstName, x.LastName, x.Email, x.PhoneNumber));
        //    CreateMap<AddBookingDTO, Booking>()
        //        .ConvertUsing(x => new Booking(x.BookingId, x.PostId, x.UserId,
        //                                         x.StartDate, x.EndDate));

        //    CreateFeedbackMapToDTO<Domain.Entities.User, UserDTO>();
        //    CreateFeedbackMapToDTO<Post, PostDetailsDTO>();
        //    CreateFeedbackMapFromDTO<Domain.Entities.User>();
        //    CreateFeedbackMapFromDTO<Post>();
        //}
        //private string DateTimeToString(DateTime? time)
        //{
        //    return time?.ToString("HH:mm");
        //}
        //private ICollection<E> MapCollection<T, E>(ICollection<T> collection,
        //    ResolutionContext context)
        //{
        //    if (collection != null)
        //    {
        //        return collection.Any() ? context.Mapper.Map<ICollection<T>, ICollection<E>>(collection)
        //                    : null;
        //    }
        //    return null;
        //}
        //private void CreateFeedbackMapFromDTO<T>() where T : FeedbackEntity
        //{
        //    CreateMap<FeedbackDTO, Feedback<T>>()
        //        .ConvertUsing(x => new Feedback<T>(x.ItemId, x.UserId, x.Rating, x.Message));
        //}
        //private void CreateFeedbackMapToDTO<T, E>() where T : FeedbackEntity
        //                                            where E : IFeedbackEntityDTO
        //{
        //    CreateMap<Feedback<T>, FeedbackInfoDTO<E>>()
        //       .ConvertUsing((x, dest, context) =>
        //       {
        //           return new FeedbackInfoDTO<E>(x.UserId, x.ItemId, x.Rating, x.Message, x.Id,
        //              x.Item == null ? default : context.Mapper.Map<E>(x.Item),
        //              x.FeedbackOwner == null ? null : context.Mapper.Map<UserDTO>(x.FeedbackOwner));
        //       });
        }
    }
}
