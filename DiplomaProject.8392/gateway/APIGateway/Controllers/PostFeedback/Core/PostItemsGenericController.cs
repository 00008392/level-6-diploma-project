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
    //[ApiController]
    //public abstract class PostItemsGenericController<T> : ControllerBase
    //    where T: IPostItems
    //{
    //    protected readonly IPostItems _client;
    //    protected readonly PostCRUD.PostCRUDClient _postClient;

    //    protected PostItemsGenericController(T client, PostCRUD.PostCRUDClient postClient)
    //    {
    //        _client = client;
    //        _postClient = postClient;
    //    }

    //    // GET: api/<AccommodationItemsGenericController>
    //    [HttpGet]
    //    public async Task<IActionResult> Get()
    //    {
    //        var reply = await _client.GetItemsAsync(new Empty());
    //        return Ok(reply.Items);
    //    }

    //    //POST api/<AccommodationItemsGenericController>
    //    [Authorize]
    //    [HttpPost("add")]
    //    public async Task<IActionResult> AddItems(AddItemsRequest request)
    //    {
    //        var post = await _postClient.GetPostByIdAsync(new Request { Id = request.PostId });
    //        if (post.Owner.Id != GetLoggedUserId())
    //        {
    //            return Unauthorized();
    //        }
    //        var addRequest = (AddItemsRequest)ConvertItems(request);
    //        var reply = await _client.AddItemsAsync(addRequest);
    //        if (!reply.IsSuccess)
    //        {
    //            return BadRequest(reply);
    //        }
    //        return StatusCode(201);
    //    }

    //    [Authorize]
    //    [HttpPost("remove")]
    //    public async Task<IActionResult> RemoveItems(RemoveItemsRequest request)
    //    {
    //        var post = await _postClient.GetPostByIdAsync(new Request { Id = request.PostId });
    //        if (post.Owner.Id != GetLoggedUserId())
    //        {
    //            return Unauthorized();
    //        }
    //        var reply = await _client.RemoveItemsAsync((RemoveItemsRequest)ConvertItems(request));
    //        if (!reply.IsSuccess)
    //        {
    //            return BadRequest(reply);
    //        }
    //        return NoContent();
    //    }

    //    private IPostItemsRequest<E> ConvertItems<E>(IPostItemsRequest<E> request)
    //    {
    //        request.Items.Add(request.ItemsJson);
    //        return request;
    //    }
    //    private int GetLoggedUserId()
    //    {
    //        return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
    //    }
    //}
}
