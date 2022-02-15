using AutoMapper;
using PostFeedback.API.Mappings.Helpers;
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
        //class for mapping domain entities to dtos and vice versa
        public PostDomainMapper()
        {
            //post
            CreateMap<PostManipulationDTO, Post>()
                .ConvertUsing(x => new Post(x.Id, x.Title, x.Description, x.OwnerId,
                DateTime.Now, x.CategoryId, x.CityId, x.Address, x.ContactNumber,
                x.RoomsNo, x.BathroomsNo, x.BedsNo, x.MaxGuestsNo, x.SquareMeters,
                x.Price, x.IsWholeApartment, ConversionHelper.DateTimeToString(x.MovingInTime),
               ConversionHelper.DateTimeToString(x.MovingOutTime)));
            CreateMap<Post, PostDetailsDTO>()
                .ConvertUsing((x, dest, context) => new PostDetailsDTO(x.Id,
                x.Title, x.Description, x.Address, x.ContactNumber, x.CategoryId, x.CityId,
                x.RoomsNo, x.BathroomsNo,
                x.BedsNo, x.MaxGuestsNo, x.SquareMeters, x.Price,
                x.IsWholeApartment, x.Owner == null ? null : context.Mapper.Map<UserDTO>(x.Owner),
                x.DatePublished, x.Category?.Name, x.City?.Name,
                x.MovingInTime, x.MovingOutTime,
                MapCollection<Domain.Entities.Photo, PhotoDTO>(x.Photos, context),
                MapCollection<Rule, ItemDTO>(x.Rules, context),
                MapCollection<Facility, ItemDTO>(x.Facilities, context),
                MapCollection<Booking, DatesBookedDTO>(x.Bookings, context)
                ));
            //post related entities
            CreateMap<Domain.Entities.User, UserDTO>()
                .ConvertUsing(x => new UserDTO(x.Id, x.FirstName, x.LastName,
                x.Email));
            CreateMap<Category, ItemDTO>()
                .ConvertUsing(x => new ItemDTO(x.Id, x.Name));
            CreateMap<City, ItemDTO>()
                .ConvertUsing(x => new ItemDTO(x.Id, x.Name));
            CreateMap<Facility, ItemDTO>()
                .ConvertUsing(x => new ItemDTO(x.Id, x.Name));
            CreateMap<Rule, ItemDTO>()
               .ConvertUsing(x => new ItemDTO(x.Id, x.Name));
            CreateMap<Booking, DatesBookedDTO>()
                .ConvertUsing(x => new DatesBookedDTO(x.StartDate, x.EndDate));
            CreateMap<Domain.Entities.Photo, PhotoDTO>()
                .ConvertUsing(x => new PhotoDTO(x.Id, x.PhotoBytes, x.MimeType));
            //feedback
            FeedbackMapToDTO<Domain.Entities.User, UserDTO>();
            FeedbackMapToDTO<Post, PostDetailsDTO>();
            FeedbackMapFromDTO<Domain.Entities.User>();
            FeedbackMapFromDTO<Post>();
        }
        //maps collection of domain entities to dtos or vice versa
        private ICollection<E> MapCollection<T, E>(ICollection<T> collection,
            ResolutionContext context)
        {
            if (collection != null)
            {
                return collection.Any() ? context.Mapper.Map<ICollection<T>, ICollection<E>>(collection)
                            : null;
            }
            return null;
        }
        //maps feedback dto to feedback domain entity
        private void FeedbackMapFromDTO<T>() where T : FeedbackEntity
        {
            CreateMap<FeedbackDTO, Feedback<T>>()
                .ConvertUsing(x => new Feedback<T>(x.ItemId, x.CreatorId, x.Rating, x.Message));
        }
        //maps feedback domain entity to feedback dto
        private void FeedbackMapToDTO<T, E>() where T : FeedbackEntity
                                                    where E : IFeedbackEntityDTO
        {
            CreateMap<Feedback<T>, FeedbackInfoDTO<E>>()
               .ConvertUsing((x, dest, context) =>
               {
                   return new FeedbackInfoDTO<E>(x.CreatorId, x.ItemId, x.Rating, x.Message, x.Id,
                      x.Item == null ? default : context.Mapper.Map<E>(x.Item),
                      x.Creator == null ? null : context.Mapper.Map<UserDTO>(x.Creator));
               });
        }
    }
}
