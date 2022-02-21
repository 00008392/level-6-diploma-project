using AutoMapper;
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
using Grpc.Base.Helpers;

namespace PostFeedback.API.Services
{
    //Post CRUD grpc service
    public class PostServiceGrpc : PostService.PostServiceBase
    {
        //inject service from domain logic layer
        private readonly IPostService _service;
        private readonly IMapper _mapper;
        public PostServiceGrpc(
            IPostService service,
            IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        //create new post
        public override async Task<Response> CreatePost(CreatePostRequest request, ServerCallContext context)
        {
            //call helper method that handles create and update grpc actions
            return await GrpcServiceHelper.HandleCreateUpdateActionAsync
                <PostManipulationDTO, Response, CreatePostRequest>(_service.CreatePostAsync, _mapper,
                request);
        }
        //update post information
        public override async Task<Response> UpdatePost(UpdatePostRequest request, ServerCallContext context)
        {
            //call helper method that handles create and update grpc actions
            return await GrpcServiceHelper.HandleCreateUpdateActionAsync
                <PostManipulationDTO, Response, UpdatePostRequest>(_service.UpdatePostAsync, _mapper,
                request);
        }
        //delete post
        public override async Task<Response> DeletePost(Request request, ServerCallContext context)
        {
            //call helper method that handles delete grpc action
            return await GrpcServiceHelper.HandleDeleteActionAsync<Response>
                (request.Id, _service.DeletePostAsync);
        }
        //retrieve post information by id
        public override async Task<PostResponse> GetPostById(Request request, ServerCallContext context)
        {
            //call helper method that handles retrieval by id grpc action
            return await GrpcServiceHelper.HandleRetrievalByIdAsync<PostResponse, PostDetailsDTO>
                 (request.Id, _service.GetPostByIdAsync, _mapper);
        }
        //retrieve list of posts by filter criteria
        public override async Task<PostListResponse> GetPosts(FilterRequest request, ServerCallContext context)
        {
            //map grpc filter class to filter 
            var filter = _mapper.Map<FilterParameters>(request);
            //get posts by filter criteria
            var posts = await _service.GetPostsAsync(filter);
            //map to response
            return GrpcServiceHelper.MapItems<PostListResponse, PostDetailsDTO, PostResponse>
                (_mapper, posts);
        }
    }

}
