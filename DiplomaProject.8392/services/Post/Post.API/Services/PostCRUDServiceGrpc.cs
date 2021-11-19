using AutoMapper;
using EventBus.Contracts;
using ExceptionHandling;
using FluentValidation;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Post.Domain.Core;
using Post.Domain.Logic.Contracts;
using Post.Domain.Logic.DTOs;
using Post.Domain.Logic.IntegrationEvents.Events;
using Protos.Common;
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
        private readonly IMapper _mapper;
        public PostCRUDServiceGrpc(IPostCRUDService service, IEventBus eventBus,
            IMapper mapper)
        {
            _service = service;
            _eventBus = eventBus;
            _mapper = mapper;
        }
        public override async Task<Response> CreatePost(CreatePostRequest request, ServerCallContext context)
        {

            
            if (request == null)
            {
                return new Response
                {
                    Message = "Empty request"
                };
            }
            var createPostDTO = _mapper.Map<CreatePostDTO>(request);

            var response = new Response();
            try
            {
                await _service.CreatePostAsync(createPostDTO);
                response.IsSuccess = true;
                var integrationEvent = _mapper.Map<AccommodationCreatedIntegrationEvent>(createPostDTO);
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
          
            if (request == null)
            {
                return new Response
                {
                    Message = "Empty request"
                };
            }
            var updatePostDTO = _mapper.Map<UpdatePostDTO>(request);
            
            var response = new Response();
            try
            {
                await _service.UpdatePostAsync(updatePostDTO);
                response.IsSuccess = true;
                var integrationEvent = _mapper.Map<AccommodationUpdatedIntegrationEvent>(updatePostDTO);
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
            var response = _mapper.Map<PostInfoResponse>(post);
            if (post.AccommodationPhotos != null)
            {
                response.AccommodationPhotos.AddRange(
                    _mapper.Map<ICollection<AccommodationPhotoDTO>, ICollection<AccommodationPhoto>>(post.AccommodationPhotos));
            }
            if (post.AccommodationRules!=null)
            {
                response.AccommodationRules.AddRange(GetItemsList(post.AccommodationRules));
            }
            if (post.AccommodationFacilities!=null)
            {
                response.AccommodationFacilities.AddRange(GetItemsList(post.AccommodationFacilities));
            }
            if (post.AccommodationSpecificities!=null)
            {
                response.AccommodationSpecificities.AddRange(GetItemsList(post.AccommodationSpecificities));
            }
            return response;
        }
        private IEnumerable<AccommodationItem> GetItemsList(ICollection<AccommodationItemInfoDTO> items) 
        {
            return _mapper.Map<ICollection<AccommodationItemInfoDTO>, ICollection<AccommodationItem>>(items);

        }

    }
   
}
