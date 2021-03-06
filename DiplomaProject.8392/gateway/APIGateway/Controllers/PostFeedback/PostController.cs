using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostFeedback.API;
using Protos.Common;
using Google.Protobuf.WellKnownTypes;
using APIGateway.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using APIGateway.Authorization.Helpers;
using Microsoft.AspNetCore.Http;
using APIGateway.Services.Contracts;
using Google.Protobuf;
using AutoMapper;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIGateway.Controllers.PostFeedback
{
    //controller for post manipulation
    [Route("api/posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        //injecting grpc client to access services of post/feedback microservice
        private readonly PostService.PostServiceClient _postClient;
        //injecting authorization service for resource based authorization
        private readonly IAuthorizationService _authorizationService;

        public PostController(
            PostService.PostServiceClient postClient,
            IAuthorizationService authorizationService)
        {
            _postClient = postClient;
            _authorizationService = authorizationService;
        }
        //get posts by filter criteria
        [HttpGet]
        public async Task<IActionResult> GetPosts([FromQuery] FilterRequest request)
        {
            //get posts
            var reply = await _postClient.GetPostsAsync(ConvertFilterData(request));
            //convert post data to proper dislay format
            reply.Items.ToList().ForEach(x => ConvertPostResponse(x));
            //return list of posts
            return Ok(reply.Items);
        }

        // GET api/<PostController>/5
        //get post details
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(long id)
        {
            //get post
            var reply = await _postClient.GetPostByIdAsync(new Request { Id = id });
            //if no post, return not found response
            if (reply.NoItem)
            {
                return NotFound("Post not found");
            }
            //convert post data to proper dislay format (TimeStamp -> DateTime) and return it
            return Ok(ConvertPostResponse(reply));
        }

        //POST api/<PostController>
        //Create new post
        //Only authorized access
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostRequest request)
        {
            //get user id from claim 
            var id = AuthorizationHelper.GetLoggedUserId(User);
            //check if user is logged in
            if (id == null)
            {
                return Unauthorized();
            }
            //assign id of logged user to post owner
            request.OwnerId = id;
            // try to create post
            var reply = await _postClient.CreatePostAsync(ConvertPostRequest(request));
            //in case of errors, return bad request
            if (!reply.IsSuccess)
            {
                return BadRequest(reply);
            }
            //if successful, return created status code
            return StatusCode(201);
        }

        // PUT api/<PostController>/5
        //Update post
        //Only authorized access
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdatePost(UpdatePostRequest request)
        {
            //assign id of logged user to post owner
            request.OwnerId = AuthorizationHelper.GetLoggedUserId(User);
            //check if user is authorized to update post
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, request.Id, "PostUpdatePolicy");
            if (!authorizationResult.Succeeded)
            {
                return Unauthorized();
            }
            //try to update post
            var reply = await _postClient.UpdatePostAsync(ConvertPostRequest(request));
            //in case of errors, return bad request
            if (!reply.IsSuccess)
            {
                return BadRequest(reply);
            }
            return NoContent();
        }

        // DELETE api/<PostController>/5
        //Delete post
        //Only authorized access
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(long id)
        {
            //check if user is authorized to delete post
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, id, "PostUpdatePolicy");
            if (!authorizationResult.Succeeded)
            {
                return Unauthorized();
            }
            //try to delete post
            var request = new Request { Id = id };
            var reply = await _postClient.DeletePostAsync(request);
            //in case of errors, return bad request
            if (!reply.IsSuccess)
            {
                return BadRequest(reply);
            }
            return NoContent();
        }
        private T ConvertPostRequest<T>(T post) where T: IPostRequest
        {
            //convert from DateTime to TimeStamp  
            post.MovingInTimeStamp = GrpcConversion.FromDateTimeToTimeStamp(post.MovingInTime);
            post.MovingOutTimeStamp = GrpcConversion.FromDateTimeToTimeStamp(post.MovingOutTime);
            return post;
        }
        private PostResponse ConvertPostResponse(PostResponse post)
        {
            //convert from TimeStamp to DateTime
            post.DatePublished = (DateTime)GrpcConversion.FromTimeStampToDateTime(post.DatePublishedTimeStamp);
            if (post.DatesBooked != null)
            {
                post.DatesBooked.ToList().ForEach(x =>
                {
                    x.StartDate = (DateTime)GrpcConversion.FromTimeStampToDateTime(x.StartDateTimeStamp);
                    x.EndDate = (DateTime)GrpcConversion.FromTimeStampToDateTime(x.EndDateTimeStamp);
                });
            }
            return post;
        }
        private FilterRequest ConvertFilterData(FilterRequest request)
        {
            //convert from date time to time stamp
            request.StartDateTimeStamp = GrpcConversion.FromDateTimeToTimeStamp(request.StartDate);
            request.EndDateTimeStamp = GrpcConversion.FromDateTimeToTimeStamp(request.EndDate);
            return request;
        }
    }
}
