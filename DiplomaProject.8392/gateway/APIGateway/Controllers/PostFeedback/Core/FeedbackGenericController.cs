using APIGateway.Authorization.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostFeedback.API;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIGateway.Controllers.PostFeedback.Core
{
    //generic controller for feedback manipulation
    [Route("api/[controller]")]
    [ApiController]
    public abstract class FeedbackGenericController<T> : ControllerBase
        where T : IFeedbackService
    {
        //injecting grpc client interface instead of concrete grpc client to access services of post/feedback microservice
        //because generic controller is not concerned whether it is client for user feedbacks or post feedbacks
        protected readonly IFeedbackService _client;
        //injecting authorization service for resource based authorization
        protected readonly IAuthorizationService _authorizationService;
        //policy name for authorization
        protected readonly string _policy;

        protected FeedbackGenericController(
            T client,
            IAuthorizationService authorizationService,
            string policy)
        {
            _client = client;
            _authorizationService = authorizationService;
            _policy = policy;
        }

        // GET: api/<FeedbackGenericController>/5
        //get feedback details
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeedback(long id)
        {
            var request = new Request
            {
                Id = id
            };
            //get feedback
            var reply = await _client.GetFeedbackDetailsAsync(request);
            //if no feedback, return not found response
            if (reply.NoItem)
            {
                return NotFound("Feedback not found");
            }
            return Ok(reply);
        }

        //get feedbacks by id of item on which feedback is left
        [HttpGet("item/{itemId}")]
        public async Task<IActionResult> GetFeedbacksForItem(long itemId)
        {
            var request = new Request
            {
                Id = itemId
            };
            //get feedbacks
            var reply = await _client.GetFeedbacksForItemAsync(request);
            return Ok(reply.Feedbacks);
        }

        // POST api/<FeedbackGenericController>
        //Create new feedback
        //Only authorized access
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> LeaveFeedback(CreateFeedbackRequest request)
        {
            //get user id from claim 
            var id = AuthorizationHelper.GetLoggedUserId(User);
            //check if user is logged in
            if (id == null)
            {
                return Unauthorized();
            }
            //assign id of logged user to feedback creator
            request.CreatorId = id;
            //try to create feedback
            var reply = await _client.LeaveFeedbackAsync(request);
            //in case of errors, return bad request
            if (!reply.IsSuccess)
            {
                return BadRequest(reply);
            }
            //if successful, return created status code
            return StatusCode(201);
        }

        // DELETE api/<FeedbackGenericController>/5
        //Delete feedback
        //Only authorized access
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedback(long id)
        {
            //check if user is authorized to delete feedback
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, id, _policy);
            if (!authorizationResult.Succeeded)
            {
                return Unauthorized();
            }
            //try to delete feedback
            var request = new Request
            {
                Id = id
            };
            var reply = await _client.DeleteFeedbackAsync(request);
            //in case of errors, return not found
            if (!reply.IsSuccess)
            {
                return NotFound(reply);
            }
            return NoContent();
        }
    }
}
