using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Post.API;
using Protos.Common;
using Google.Protobuf.WellKnownTypes;
using APIGateway.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIGateway.Controllers.PostFeedback
{
    [Route("api/posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly PostCRUD.PostCRUDClient _postClient;


        public PostController(PostCRUD.PostCRUDClient postClient)
        {
            _postClient = postClient;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] FilterRequest request)
        {
            var reply = await _postClient.GetPostsAsync(request);
            reply.Items.ToList().ForEach(x => ConvertPostData(x));
            return Ok(reply.Items);
        }

        // GET api/<PostController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var reply = await _postClient.GetPostByIdAsync(new Request { Id = id });
            if (reply.NoItem)
            {
                return NotFound("Post not found");
            }
            return Ok(ConvertPostData(reply));
        }

        //POST api/<PostController>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(CreatePostRequest request)
        {
           if(request.OwnerId!=GetLoggedUserId())
            {
                return Unauthorized();
            }
            var reply = await _postClient.CreatePostAsync((CreatePostRequest)ConvertPostData(request));
            if (!reply.IsSuccess)
            {
                return BadRequest(reply);
            }
            return StatusCode(201);
        }

        // PUT api/<PostController>/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, UpdatePostRequest request)
        {
            if(request.OwnerId!=GetLoggedUserId())
            {
                return Unauthorized();
            }
            if (id != request.Id)
            {
                return BadRequest("Invalid accommodation post");
            }
           
            var reply = await _postClient.UpdatePostAsync((UpdatePostRequest)ConvertPostData(request));
            if (!reply.IsSuccess)
            {
                return BadRequest(reply);
            }
            return NoContent();
        }

        // DELETE api/<PostController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var request = new Request { Id = id };
            var post = await _postClient.GetPostByIdAsync(request);
            if(post.Owner.Id!=GetLoggedUserId())
            {
                return Unauthorized();
            }
            var reply = await _postClient.DeletePostAsync(request);
            if (!reply.IsSuccess)
            {
                return BadRequest(reply);
            }
            return NoContent();
        }
        private PostInfoResponse ConvertPostData(PostInfoResponse post)
        {
            post.DatePublished = (DateTime)DateTimeConversion.FromTimeStampToDateTime(post.DatePublishedTimeStamp);
            return post;
        }
        private IPostRequest ConvertPostData(IPostRequest post)
        {
            post.MovingInTimeStamp = DateTimeConversion.FromDateTimeToTimeStamp(post.MovingInTime);
            post.MovingOutTimeStamp = DateTimeConversion.FromDateTimeToTimeStamp(post.MovingOutTime);
            return post;
        }
        private int GetLoggedUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}
