using Microsoft.AspNetCore.Mvc;
using Post.API;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIGateway.Controllers.PostFeedback.Core
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class FeedbackGenericController<T> : ControllerBase
        where T: IFeedbackService
    {
        private readonly IFeedbackService _client;
        protected FeedbackGenericController(T client)
        {
            _client = client;
        }
        // GET: api/<FeedbackGenericController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(long id)
        {
            var request = new Request
            {
                Id = id
            };
            var reply = await _client.GetFeedbackDetailsAsync(request);
            if(reply.NoFeedback)
            {
                return NotFound("Feedback not found");
            }
            return Ok(reply);
        }

        // GET api/<FeedbackGenericController>
        [HttpGet("for/{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var request = new Request
            {
                Id = id
            };
            var reply = await _client.GetFeedbacksAsync(request);
            return Ok(reply);
        }

        // POST api/<FeedbackGenericController>
        [HttpPost]
        public async Task<IActionResult> Post(CreateFeedbackRequest request)
        {
            var reply = await _client.LeaveFeedbackAsync(request);
            if (!reply.IsSuccess)
            {
                return BadRequest(reply);
            }
            return StatusCode(201);
        }

        // DELETE api/<FeedbackGenericController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var request = new Request
            {
                Id = id
            };
            var reply = await _client.DeleteFeedbackAsync(request);
            if (!reply.IsSuccess)
            {
                return NotFound(reply);
            }
            return NoContent();
        }
    }
}
