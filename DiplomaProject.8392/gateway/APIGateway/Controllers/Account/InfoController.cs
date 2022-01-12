using Account.API;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIGateway.Controllers.Account
{
    [Route("api/info")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private readonly UserInfo.UserInfoClient _infoClient;

        public InfoController(UserInfo.UserInfoClient infoClient)
        {
            _infoClient = infoClient;
        }

        // GET: api/<CountryController>
        [HttpGet("countries")]
        public async Task<IActionResult> Get()
        {
            var reply = await _infoClient.GetAllCountriesAsync(new Protos.Common.Empty());
            return Ok(reply.Items);
        }

     
    }
}
