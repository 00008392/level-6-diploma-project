using Microsoft.AspNetCore.Mvc;
using PostFeedback.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIGateway.Controllers.PostFeedback
{
    //[Route("api/info")]
    //[ApiController]
    //public class InfoController : ControllerBase
    //{
    //    private readonly PostInfo.PostInfoClient _infoClient;

    //    public InfoController(PostInfo.PostInfoClient infoClient)
    //    {
    //        _infoClient = infoClient;
    //    }

    //    // GET: api/<CityController>
    //    [HttpGet("cities")]
    //    public async Task<IActionResult> GetCities()
    //    {
    //        var reply = await _infoClient.GetAllCitiesAsync(new Protos.Common.Empty());
    //        return Ok(reply.Items);
    //    }
    //    [HttpGet("categories")]
    //    public async Task<IActionResult> GetCategories()
    //    {
    //        var reply = await _infoClient.GetAllCategoriesAsync(new Protos.Common.Empty());
    //        return Ok(reply.Items);
    //    }
    //}
}
