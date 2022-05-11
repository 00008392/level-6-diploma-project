using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using PostFeedback.API;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIGateway.Controllers.PostFeedback
{
    //controller for getting info related to post
    [Route("api/posts/info")]
    [ApiController]
    public class PostRelatedInfoController : ControllerBase
    {
        //injecting grpc client to access services of post/feedback microservice
        private readonly PostRelatedInfoService.PostRelatedInfoServiceClient _infoClient;

        public PostRelatedInfoController(PostRelatedInfoService.PostRelatedInfoServiceClient infoClient)
        {
            _infoClient = infoClient;
        }

        // GET: api/<CityController>
        //get all cities
        [HttpGet("cities")]
        public async Task<IActionResult> GetCities()
        {
            return await GetAllItemsAsync(_infoClient.GetAllCitiesAsync);
        }
        //get all categories
        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            return await GetAllItemsAsync(_infoClient.GetAllCategoriesAsync);
        }
        //get all rules
        [HttpGet("rules")]
        public async Task<IActionResult> GetRules()
        {
            return await GetAllItemsAsync(_infoClient.GetAllRulesAsync);
        }
        //get all facilities
        [HttpGet("facilities")]
        public async Task<IActionResult> GetFacilities()
        {
            return await GetAllItemsAsync(_infoClient.GetAllFacilitiesAsync);
        }
        //method to get post related items
        private async Task<IActionResult> GetAllItemsAsync(Func<Empty, Metadata, DateTime?, CancellationToken, AsyncUnaryCall<ItemListResponse>> action)
        {
            //action - grpc service method
            var reply = await action(new Empty(), null, null, default);
            return Ok(reply.Items);
        }
    }
}
