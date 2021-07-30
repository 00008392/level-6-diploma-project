using FluentValidation;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Post.API.ExceptionHandling;
using Post.Domain.Core;
using Post.Domain.Logic.Contracts;
using Post.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post.API.Services
{
    public class PostCRUDServiceGrpc: PostCRUD.PostCRUDBase
    {
        private readonly IPostCRUDService _service;
        public PostCRUDServiceGrpc(IPostCRUDService service)
        {
            _service = service;
        }
        public override async Task<Response> CreatePost(CreatePostRequest request, ServerCallContext context)
        {
            var baseRequest = request.BaseRequest;
            var createPostDTO = new CreatePostDTO
            {
                Title = baseRequest.Title,
                Description = baseRequest.Description,
                OwnerId = baseRequest.OwnerId,
                CategoryId = baseRequest.CategoryId,
                Address = baseRequest.Address,
                ReferencePoint = baseRequest.ReferencePoint,
                ContactNumber = baseRequest.ContactNumber,
                RoomsNo = baseRequest.RoomsNo,
                BathroomsNo = baseRequest.BathroomsNo,
                BedsNo = baseRequest.BedsNo,
                MaxGuestsNo = baseRequest.MaxGuestsNo,
                SquareMeters = baseRequest.SquareMeters,
                Price = (decimal)baseRequest.Price,
                Latitude = (decimal)baseRequest.Latitude,
                Longitude = (decimal)baseRequest.Longitude,
                IsWholeApartment = baseRequest.IsWholeApartment,
                AdditionalInfo = baseRequest.AdditionalInfo
            };
            try
            {
                await _service.CreatePostAsync(createPostDTO);
                return new Response
                {
                    IsSuccess = true
                };
            }
            catch (ValidationException ex)
            {
                return ExceptionHandler.HandleValidationException(ex);
            }
            catch (Exception ex)
            {
                return ExceptionHandler.HandleException(ex);
            }
        }
        public override async Task<Response> UpdatePost(UpdatePostRequest request, ServerCallContext context)
        {
            var baseRequest = request.BaseRequest;
            var updatePostDTO = new UpdatePostDTO
            {
                Id = request.Id,
                Title = baseRequest.Title,
                Description = baseRequest.Description,
                OwnerId = baseRequest.OwnerId,
                CategoryId = baseRequest.CategoryId,
                Address = baseRequest.Address,
                ReferencePoint = baseRequest.ReferencePoint,
                ContactNumber = baseRequest.ContactNumber,
                RoomsNo = baseRequest.RoomsNo,
                BathroomsNo = baseRequest.BathroomsNo,
                BedsNo = baseRequest.BedsNo,
                MaxGuestsNo = baseRequest.MaxGuestsNo,
                SquareMeters = baseRequest.SquareMeters,
                Price = (decimal)baseRequest.Price,
                Latitude = (decimal)baseRequest.Latitude,
                Longitude = (decimal)baseRequest.Longitude,
                IsWholeApartment = baseRequest.IsWholeApartment,
                AdditionalInfo = baseRequest.AdditionalInfo
            };
            try
            {
                await _service.UpdatePostAsync(updatePostDTO);
                return new Response
                {
                    IsSuccess = true
                };
            }
            catch (ValidationException ex)
            {
                return ExceptionHandler.HandleValidationException(ex);
            }
            catch (Exception ex)
            {
                return ExceptionHandler.HandleException(ex);
            }
        }
        public override async Task<Response> DeletePost(Request request, ServerCallContext context)
        {
            try
            {
                await _service.DeletePostAsync(request.Id);
                return new Response
                {
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return ExceptionHandler.HandleException(ex);
            }
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
                Latitude = (double)post.Latitude,
                Longitude = (double)post.Longitude,
                IsWholeApartment = post.IsWholeApartment,
                AdditionalInfo = post.AdditionalInfo,
                DatePublished = Timestamp.FromDateTime(DateTime.SpecifyKind(post.DatePublished, DateTimeKind.Utc))
            };
            if (post.AccommodationPhotos != null)
            {
                response.AccommodationPhotos.AddRange(post.AccommodationPhotos.Select(x => new AccommodationPhoto
                {
                    Id = x.Id,
                    AccommodationId = x.AccommodationId,
                    Photo = x.Photo == null ? null : Google.Protobuf.ByteString.CopyFrom(x.Photo),
                    MimeType = x.MimeType
                }));
            }
            if (post.AccommodationRules != null)
            {
                response.AccommodationRules.AddRange(GetItemsList<Post.Domain.Entities.AccommodationRule>(post.AccommodationRules));
            }
            if (post.AccommodationFacilities != null)
            {
                response.AccommodationFacilities.AddRange(GetItemsList<Post.Domain.Entities.AccommodationFacility>(post.AccommodationFacilities));
            }
            if (post.AccommodationSpecificities != null)
            {
                response.AccommodationSpecificities.AddRange(GetItemsList<Post.Domain.Entities.AccommodationSpecificity>(post.AccommodationSpecificities));
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
                Base =
                {
                    Id = x.Item.Id,
                    Name = x.Item.Name
                }
            });

        }

    }
   
}
