using AutoMapper;
using Post.Domain.Core;
using Post.Domain.Entities;
using Post.Domain.Logic.DTOs;
using Post.Domain.Logic.DTOs.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post.API.Mappings
{
    public class PostDomainMapper: Profile
    {
        public PostDomainMapper()
        {
            CreateMap<CreatePostDTO, Accommodation>()
               .ConvertUsing(x => new Accommodation(x.Title, x.Description, x.OwnerId,
                DateTime.Now, x.CategoryId, x.Address, x.ReferencePoint, x.ContactNumber,
                x.RoomsNo, x.BathroomsNo, x.BedsNo, x.MaxGuestsNo, x.SquareMeters,
                x.Price, x.Latitude, x.Longitude, x.IsWholeApartment, DateTimeToString(x.MovingInTime),
               DateTimeToString(x.MovingOutTime), x.AdditionalInfo));
            CreateMap<Domain.Entities.User, UserDTO>()
                .ConvertUsing(x => new UserDTO(x.Id, x.FirstName, x.LastName,
                x.Email, x.PhoneNumber));
            CreateMap<Domain.Entities.Category, CategoryDTO>()
                .ConvertUsing(x => new CategoryDTO(x.Id, x.Name));
            CreateMap<Domain.Entities.AccommodationPhoto, AccommodationPhotoDTO>()
                .ConvertUsing(x => new AccommodationPhotoDTO(x.Id, x.Photo, x.MimeType));
            CreateMap<ItemBase, ItemInfoDTO>()
                .ConvertUsing(x => new ItemInfoDTO(x.Id, x.Name));
            CreateMap<ItemAccommodationBase, AccommodationItemInfoDTO>()
                .ConvertUsing((x, dest, context) => new AccommodationItemInfoDTO(x.Id,
                x.OtherItem, context.Mapper.Map<ItemInfoDTO>(x.Item))
                );
            CreateMap<AccommodationFacility, AccommodationItemInfoDTO>()
                .IncludeBase<ItemAccommodationBase, AccommodationItemInfoDTO>();
            CreateMap<AccommodationRule, AccommodationItemInfoDTO>()
                .IncludeBase<ItemAccommodationBase, AccommodationItemInfoDTO>(); 
            CreateMap<AccommodationSpecificity, AccommodationItemInfoDTO>()
                .IncludeBase<ItemAccommodationBase, AccommodationItemInfoDTO>(); 
            CreateMap<Accommodation, AccommodationInfoDTO>()
                .ConvertUsing((x,dest, context) => new AccommodationInfoDTO(x.Id,
                x.Title, x.Description, x.OwnerId, x.CategoryId, x.Address,
                x.ReferencePoint, x.ContactNumber, x.RoomsNo, x.BathroomsNo,
                x.BedsNo, x.MaxGuestsNo, x.SquareMeters, x.Price, x.Latitude,
                x.Longitude, x.IsWholeApartment, x.AdditionalInfo, context.Mapper.Map<UserDTO>(x.Owner),
                x.DatePublished, x.Category == null ? null : context.Mapper.Map<CategoryDTO>(x.Category),
                x.MovingInTime, x.MovingOutTime,
                MapCollection<Domain.Entities.AccommodationPhoto, AccommodationPhotoDTO>(x.AccommodationPhotos, context),
                MapCollection<AccommodationSpecificity, AccommodationItemInfoDTO>(x.AccommodationSpecificities, context),
                MapCollection<AccommodationRule, AccommodationItemInfoDTO>(x.AccommodationRules, context),
                MapCollection<AccommodationFacility, AccommodationItemInfoDTO>(x.AccommodationFacilities, context)
                ));

            CreateMap<CreateUserDTO, Domain.Entities.User>()
                .ConvertUsing(x => new Domain.Entities.User(x.Email, x.FirstName, x.LastName));
            CreateMap<UpdateUserDTO, Domain.Entities.User>()
                .ConvertUsing(x => new Domain.Entities.User(x.Id, x.FirstName,
                x.LastName, x.Email, x.PhoneNumber));

            CreateMap<AddBookingDTO, DatesBooked>()
                .ConvertUsing(x => new DatesBooked(x.BookingId, x.AccommodationId,
                                                 x.StartDate, x.EndDate));

            CreateFeedbackMapToDTO<User, UserDTO>();
            CreateFeedbackMapToDTO<Accommodation, AccommodationInfoDTO>();
            CreateFeedbackMapFromDTO<User>();
            CreateFeedbackMapFromDTO<Accommodation>();
        }
        private string DateTimeToString(DateTime? time)
        {
            return time?.ToString("HH:mm");
        }
        private ICollection<E> MapCollection<T, E>(ICollection<T> collection, 
            ResolutionContext context)
        {
            return collection.Any() ? context.Mapper.Map<ICollection<T>, ICollection<E>>(collection)
                            : null;
        }
        private void CreateFeedbackMapFromDTO<T>() where T: FeedbackEntity
        {
            CreateMap<FeedbackDTO, Feedback<T>>()
                .ConvertUsing(x => new Feedback<T>(x.ItemId, x.UserId, x.Rating, x.Message));
        }
        private void CreateFeedbackMapToDTO<T, E>() where T: FeedbackEntity
                                                    where E : IFeedbackEntityDTO
        {
            CreateMap<Feedback<T>, FeedbackInfoDTO<E>>()
               .ConvertUsing((x, dest, context) => {
                   return new FeedbackInfoDTO<E>(x.UserId, x.ItemId, x.Rating, x.Message, x.Id,
                      x.Item == null ? default(E) : context.Mapper.Map<E>(x.Item),
                      x.FeedbackOwner == null ? null : context.Mapper.Map<UserDTO>(x.FeedbackOwner));
               });
        }
    }
}
