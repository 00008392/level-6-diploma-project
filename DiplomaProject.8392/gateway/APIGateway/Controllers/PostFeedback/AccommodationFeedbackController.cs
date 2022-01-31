using APIGateway.Controllers.PostFeedback.Core;
using Microsoft.AspNetCore.Mvc;
using PostFeedback.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIGateway.Controllers.PostFeedback
{
    [Route("api/feedbacks/accommodation")]
    [ApiController]
    public class AccommodationFeedbackController : FeedbackGenericController<FeedbackForAccommodation.FeedbackForAccommodationClient>
    {
        public AccommodationFeedbackController(FeedbackForAccommodation.FeedbackForAccommodationClient client):
            base(client)
        {

        }
        
    }
}
