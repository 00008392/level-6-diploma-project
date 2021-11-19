using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Post.API;
using Protos.Common;
using Google.Protobuf.WellKnownTypes;
using APIGateway.Helpers;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIGateway.Controllers.PostFeedback
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly PostCRUD.PostCRUDClient _postClient;


        public PostController(PostCRUD.PostCRUDClient postClient)
        {
            _postClient = postClient;
        }

        //TO DO filtering
        // GET: api/<PostController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<PostController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(long id)
        {
            var reply = ConvertPostData(await _postClient.GetPostByIdAsync(new Request { Id = id }));
            return Ok(reply);
        }

        //POST api/<PostController>
        [HttpPost]
        public async Task<IActionResult> PostAccommodation(CreatePostRequest request)
        {
           
            var reply = await _postClient.CreatePostAsync((CreatePostRequest)ConvertPostData(request));
            if (!reply.IsSuccess)
            {
                return BadRequest(reply);
            }
            return StatusCode(201);
        }

        // PUT api/<PostController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccommodation(int id, UpdatePostRequest request)
        {
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var reply = await _postClient.DeletePostAsync(new Request { Id = id });
            if (!reply.IsSuccess)
            {
                return NotFound(reply);
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
    }
}
