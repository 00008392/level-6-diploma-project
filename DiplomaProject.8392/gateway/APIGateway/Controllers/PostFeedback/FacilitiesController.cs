using APIGateway.Controllers.PostFeedback.Core;
using Microsoft.AspNetCore.Mvc;
using Post.API;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIGateway.Controllers.PostFeedback
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilitiesController : ItemsBaseController
    {
        public FacilitiesController(PostItemsInfo.PostItemsInfoClient itemsInfo,
            PostItemsManipulation.PostItemsManipulationClient itemsManipulation)
            : base(itemsInfo, itemsManipulation)
        {

        }
        [HttpGet]
        public override async Task<IActionResult> GetItems()
        {
            return Ok(await _itemsInfoClient.GetFacilitiesAsync(new Empty()));
        }
        [HttpPost]
        public async override Task<IActionResult> PostItems(AddItemsRequest request)
        {
            var reply = await _itemsManipulationClient.AddFacilitiesAsync(request);
            if (!reply.IsSuccess)
            {
                return BadRequest(reply);
            }
            return StatusCode(201);
        }
        [HttpDelete("{id}")]
        public override async Task<IActionResult> DeleteItems(RemoveItemsRequest request)
        {
            var reply = await _itemsManipulationClient.RemoveFacilitiesAsync(request);
            if (!reply.IsSuccess)
            {
                return NotFound(reply);
            }
            return NoContent();
        }
    }
}
