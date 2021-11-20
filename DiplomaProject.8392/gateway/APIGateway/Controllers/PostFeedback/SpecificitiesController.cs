using APIGateway.Controllers.PostFeedback.Core;
using Microsoft.AspNetCore.Mvc;
using Post.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIGateway.Controllers.PostFeedback
{
    [Route("api/post/specificities")]
    [ApiController]
    public class SpecificitiesController : AccommodationItemsGenericController<AccommodationSpecificities.AccommodationSpecificitiesClient>
    {
        public SpecificitiesController(AccommodationSpecificities.AccommodationSpecificitiesClient client) : base(client)
        {
        }
    }
}
