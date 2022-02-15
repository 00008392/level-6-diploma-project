using APIGateway.Controllers.PostFeedback.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostFeedback.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIGateway.Controllers.PostFeedback
{
    //child feedback controller for manipulation of feedbacks for posts
    [Route("api/post-feedbacks")]
    [ApiController]
    public class PostFeedbackController : FeedbackGenericController<FeedbackForPost.FeedbackForPostClient>
    {
        public PostFeedbackController(
            //passing concrete client
            FeedbackForPost.FeedbackForPostClient client,
            IAuthorizationService authorizationService) :
            base(
                client,
                authorizationService,
                //policy specific to post feedbacks
                "PostFeedbackDeletePolicy")
        {
        }

    }
}
