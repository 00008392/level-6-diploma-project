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
    [ApiController]
    public abstract class AccommodationItemsGenericController<T> : ControllerBase
        where T: IAccommodationItems
    {
        private readonly IAccommodationItems _client;

        protected AccommodationItemsGenericController(T client)
        {
            _client = client;
        }

        // GET: api/<AccommodationItemsGenericController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var reply = await _client.GetItemsAsync(new Empty());
            return Ok(reply);
        }

        // POST api/<AccommodationItemsGenericController>
        [HttpPost]
        public async Task<IActionResult> Post(AddItemsRequest request)
        {
            var reply = await _client.AddItemsAsync((AddItemsRequest)ConvertItems(request));
            if (!reply.IsSuccess)
            {
                return BadRequest(reply);
            }
            return StatusCode(201);
        }

        // PUT api/<AccommodationItemsGenericController>/5
        [HttpDelete]
        public async Task<IActionResult> Delete(RemoveItemsRequest request)
        {
            var reply = await _client.RemoveItemsAsync((RemoveItemsRequest)ConvertItems(request));
            if (!reply.IsSuccess)
            {
                return NotFound(reply);
            }
            return NoContent();
        }

        private IAccommodationItemsRequest<E> ConvertItems<E>(IAccommodationItemsRequest<E> request)
        {
            request.Items.Add(request.ItemsJson);
            return request;
        } 

    }
}
