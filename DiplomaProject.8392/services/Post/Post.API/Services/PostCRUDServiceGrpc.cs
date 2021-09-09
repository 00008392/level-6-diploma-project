using EventBus.Contracts;
using ExceptionHandling;
using FluentValidation;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Post.Domain.Core;
using Post.Domain.Logic.Contracts;
using Post.Domain.Logic.DTOs;
using Post.Domain.Logic.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post.API.Services
{
    public class PostCRUDServiceGrpc: PostCRUD.PostCRUDBase
    {
        private readonly IPostCRUDService _service;
        private readonly IEventBus _eventBus;
        public PostCRUDServiceGrpc(IPostCRUDService service, IEventBus eventBus)
        {
            _service = service;
            _eventBus = eventBus;
        }
        public override async Task<Response> CreatePost(CreatePostRequest request, ServerCallContext context)
        {

            var baseRequest = request.BaseRequest;
            if (baseRequest == null)
            {
                return new Response
                {
                    Message = "Empty request"
                };
            } 
            var createPostDTO = new CreatePostDTO
            {
                Title = baseRequest.Title,
                Description = baseRequest.Description,
                OwnerId = baseRequest.OwnerId ?? 0,
                CategoryId = baseRequest.CategoryId,
                Address = baseRequest.Address,
                ReferencePoint = baseRequest.ReferencePoint,
                ContactNumber = baseRequest.ContactNumber,
                RoomsNo = baseRequest.RoomsNo,
                BathroomsNo = baseRequest.BathroomsNo,
                BedsNo = baseRequest.BedsNo,
                MaxGuestsNo = baseRequest.MaxGuestsNo ?? 0,
                SquareMeters = baseRequest.SquareMeters,
                Price = (decimal)(baseRequest.Price ?? 0),
                Latitude = (decimal?)baseRequest.Latitude,
                Longitude = (decimal?)baseRequest.Longitude,
                IsWholeApartment = baseRequest.IsWholeApartment,
                MovingInTime = baseRequest.MovingInTime == null ? null : baseRequest.MovingInTime.ToDateTime(),
                MovingOutTime = baseRequest.MovingOutTime == null ? null : baseRequest.MovingOutTime.ToDateTime(),
                AdditionalInfo = baseRequest.AdditionalInfo
            };
            var response = new Response();
            try
            {
                await _service.CreatePostAsync(createPostDTO);
                response.IsSuccess = true;
                var integrationEvent = new AccommodationCreatedIntegrationEvent(createPostDTO.Title,
                    createPostDTO.Description, createPostDTO.OwnerId, createPostDTO.CategoryId,
                    createPostDTO.Address, createPostDTO.ReferencePoint, createPostDTO.ContactNumber,
                    createPostDTO.RoomsNo, createPostDTO.BathroomsNo, createPostDTO.BedsNo, createPostDTO.MaxGuestsNo,
                    createPostDTO.SquareMeters, createPostDTO.Price, createPostDTO.Latitude, createPostDTO.Longitude,
                    createPostDTO.IsWholeApartment, createPostDTO.MovingInTime.ToString(), 
                    createPostDTO.MovingOutTime.ToString(), createPostDTO.AdditionalInfo, DateTime.Now);
                _eventBus.Publish(integrationEvent);
            }
            catch (ValidationException ex)
            {
                response.HandleValidationException(ex);
            }
            catch (Exception ex)
            {
                response.HandleException(ex);
            }
            return response;
        }
        public override async Task<Response> UpdatePost(UpdatePostRequest request, ServerCallContext context)
        {
            var baseRequest = request.BaseRequest;
            var updatePostDTO = new UpdatePostDTO
            {
                Id = request.Id,
                Title = baseRequest.Title,
                Description = baseRequest.Description,
                OwnerId = baseRequest.OwnerId ?? 0,
                CategoryId = baseRequest.CategoryId,
                Address = baseRequest.Address,
                ReferencePoint = baseRequest.ReferencePoint,
                ContactNumber = baseRequest.ContactNumber,
                RoomsNo = baseRequest.RoomsNo,
                BathroomsNo = baseRequest.BathroomsNo,
                BedsNo = baseRequest.BedsNo,
                MaxGuestsNo = baseRequest.MaxGuestsNo ?? 0,
                SquareMeters = baseRequest.SquareMeters,
                Price = (decimal)(baseRequest.Price ?? 0),
                Latitude = (decimal?)baseRequest.Latitude,
                Longitude = (decimal?)baseRequest.Longitude,
                IsWholeApartment = baseRequest.IsWholeApartment,
                MovingInTime = baseRequest.MovingInTime == null ? null : baseRequest.MovingInTime.ToDateTime(),
                MovingOutTime = baseRequest.MovingOutTime == null ? null : baseRequest.MovingOutTime.ToDateTime(),
                AdditionalInfo = baseRequest.AdditionalInfo
            };
            var response = new Response();
            try
            {
                await _service.UpdatePostAsync(updatePostDTO);
                response.IsSuccess = true;
                var integrationEvent = new AccommodationUpdatedIntegrationEvent(
                    updatePostDTO.Title,
                    updatePostDTO.Description, updatePostDTO.OwnerId, updatePostDTO.CategoryId,
                    updatePostDTO.Address, updatePostDTO.ReferencePoint, updatePostDTO.ContactNumber,
                    updatePostDTO.RoomsNo, updatePostDTO.BathroomsNo, updatePostDTO.BedsNo, updatePostDTO.MaxGuestsNo,
                    updatePostDTO.SquareMeters, updatePostDTO.Price, updatePostDTO.Latitude, updatePostDTO.Longitude,
                    updatePostDTO.IsWholeApartment, updatePostDTO.MovingInTime.ToString(),
                    updatePostDTO.MovingOutTime.ToString(), updatePostDTO.AdditionalInfo, updatePostDTO.Id
                    );
                _eventBus.Publish(integrationEvent);
            }
            catch (ValidationException ex)
            {
                response.HandleValidationException(ex);
            }
            catch (Exception ex)
            {
                response.HandleException(ex);
            }
            return response;
        }
        public override async Task<Response> DeletePost(Request request, ServerCallContext context)
        {
            var response = new Response();
            try
            {
                await _service.DeletePostAsync(request.Id);
                response.IsSuccess = true;
                var integrationEvent = new AccommodationDeletedIntegrationEvent(request.Id);
                _eventBus.Publish(integrationEvent);
            }
            catch (Exception ex)
            {
                response.HandleException(ex);
            }
            return response;
        }
        public override async Task<PostInfoResponse> GetPostById(Request request, ServerCallContext context)
        {
            var post = await _service.GetPostByIdAsync(request.Id);
            if (post == null)
            {
                return new PostInfoResponse
                {
                    NoItem = true
                };
            }
            var owner = post.Owner;
            var category = post.Category;
            var response = new PostInfoResponse
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                Owner = new Owner
                {
                    Id = owner.Id,
                    FirstName = owner.FirstName,
                    LastName = owner.LastName,
                    Email = owner.Email,
                    PhoneNumber = owner.PhoneNumber
                },
                Category = category == null ? null : new Category
                {
                    Id = category.Id,
                    Name = category.Name
                },
                Address = post.Address,
                ReferencePoint = post.ReferencePoint,
                ContactNumber = post.ContactNumber,
                RoomsNo = post.RoomsNo,
                BathroomsNo = post.BathroomsNo,
                BedsNo = post.BedsNo,
                MaxGuestsNo = post.MaxGuestsNo,
                SquareMeters = post.SquareMeters,
                Price = (double)post.Price,
                Latitude = (double?)post.Latitude,
                Longitude = (double?)post.Longitude,
                IsWholeApartment = post.IsWholeApartment,
                AdditionalInfo = post.AdditionalInfo,
                MovingInTime = post.MovingInTime,
                MovingOutTime = post.MovingOutTime,
                DatePublished = Timestamp.FromDateTime(DateTime.SpecifyKind(post.DatePublished, DateTimeKind.Utc))
            };
            if (post.AccommodationPhotos.Count != 0)
            {
                response.AccommodationPhotos.AddRange(post.AccommodationPhotos.Select(x => new AccommodationPhoto
                {
                    Id = x.Id,
                    AccommodationId = x.AccommodationId,
                    Photo = x.Photo == null ? null : Google.Protobuf.ByteString.CopyFrom(x.Photo),
                    MimeType = x.MimeType
                }));
            }
            if (post.AccommodationRules.Count != 0)
            {
                response.AccommodationRules.AddRange(GetItemsList(post.AccommodationRules));
            }
            if (post.AccommodationFacilities.Count != 0)
            {
                response.AccommodationFacilities.AddRange(GetItemsList(post.AccommodationFacilities));
            }
            if (post.AccommodationSpecificities.Count != 0)
            {
                response.AccommodationSpecificities.AddRange(GetItemsList(post.AccommodationSpecificities));
            }
            return response;
        }
        private IEnumerable<AccommodationItem> GetItemsList<T>(ICollection<T> items) where T : ItemAccommodationBase
        {
            return items.Select(x => new AccommodationItem
            {
                Id = x.Id,
                AccommodationId = x.AccommodationId,
                OtherValue = x.OtherItem,
                Base = new Item
                {
                    Id = x.Item.Id,
                    Name = x.Item.Name
                }
            });

        }

    }
   
}
