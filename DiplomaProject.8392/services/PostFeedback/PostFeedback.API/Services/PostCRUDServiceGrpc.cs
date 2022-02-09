using AutoMapper;
using EventBus.Contracts;
using API.ExceptionHandling;
using FluentValidation;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using PostFeedback.Domain.Logic.Contracts;
using PostFeedback.Domain.Logic.DTOs;
using PostFeedback.Domain.Logic.Filter;
using PostFeedback.Domain.Logic.IntegrationEvents.Events;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostFeedback.API.Services
{
    //public class PostCRUDServiceGrpc : PostCRUD.PostCRUDBase
    //{
    //    private readonly IPostService _service;
    //    private readonly IEventBus _eventBus;
    //    private readonly IMapper _mapper;
    //    public PostCRUDServiceGrpc(IPostService service, IEventBus eventBus,
    //        IMapper mapper)
    //    {
    //        _service = service;
    //        _eventBus = eventBus;
    //        _mapper = mapper;
    //    }
    //    public override async Task<Response> CreatePost(CreatePostRequest request, ServerCallContext context)
    //    {


    //        if (request == null)
    //        {
    //            return new Response
    //            {
    //                Message = "Empty request"
    //            };
    //        }
    //        var createPostDTO = _mapper.Map<PostManipulationDTO>(request);

    //        var response = new Response();
    //        try
    //        {
    //            await _service.CreatePostAsync(createPostDTO);
    //            response.IsSuccess = true;
    //            var integrationEvent = _mapper.Map<PostCreatedIntegrationEvent>(createPostDTO);
    //            _eventBus.Publish(integrationEvent);
    //        }
    //        catch (ValidationException ex)
    //        {
    //            response.HandleValidationException(ex);
    //        }
    //        catch (Exception ex)
    //        {
    //            response.HandleException(ex);
    //        }
    //        return response;
    //    }
    //    public override async Task<Response> UpdatePost(UpdatePostRequest request, ServerCallContext context)
    //    {

    //        if (request == null)
    //        {
    //            return new Response
    //            {
    //                Message = "Empty request"
    //            };
    //        }
    //        var updatePostDTO = _mapper.Map<PostManipulationDTO>(request);

    //        var response = new Response();
    //        try
    //        {
    //            await _service.UpdatePostAsync(updatePostDTO);
    //            response.IsSuccess = true;
    //            var integrationEvent = _mapper.Map<PostUpdatedIntegrationEvent>(updatePostDTO);
    //            _eventBus.Publish(integrationEvent);
    //        }
    //        catch (ValidationException ex)
    //        {
    //            response.HandleValidationException(ex);
    //        }
    //        catch (Exception ex)
    //        {
    //            response.HandleException(ex);
    //        }
    //        return response;
    //    }
    //    public override async Task<Response> DeletePost(Request request, ServerCallContext context)
    //    {
    //        var response = new Response();
    //        try
    //        {
    //            await _service.DeletePostAsync(request.Id);
    //            response.IsSuccess = true;
    //            var integrationEvent = new PostDeletedIntegrationEvent(request.Id);
    //            _eventBus.Publish(integrationEvent);
    //        }
    //        catch (Exception ex)
    //        {
    //            response.HandleException(ex);
    //        }
    //        return response;
    //    }
    //    public override async Task<PostResponse> GetPostById(Request request, ServerCallContext context)
    //    {
    //        var post = await _service.GetPostByIdAsync(request.Id);
    //        if (post == null)
    //        {
    //            return new PostResponse
    //            {
    //                NoItem = true
    //            };
    //        }
    //        var response = _mapper.Map<PostResponse>(post);

    //        return response;
    //    }
    //    public override async Task<PostList> GetPosts(FilterRequest request, ServerCallContext context)
    //    {
    //        var filter = _mapper.Map<FilterParameters>(request);
    //        var posts = await _service.GetAllPostsAsync(filter);
    //        var postList = _mapper.Map<ICollection<PostResponse>>(posts);
    //        var response = new PostList();
    //        response.Items.Add(postList);
    //        return response;
    //    }

    //}

}
