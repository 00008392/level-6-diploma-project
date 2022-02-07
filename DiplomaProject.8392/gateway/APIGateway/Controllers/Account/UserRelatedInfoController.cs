using Account.API;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIGateway.Controllers.Account
{
    //controller for getting info related to user
    [Route("api/users/info")]
    [ApiController]
    public class UserRelatedInfoController : ControllerBase
    {
        private readonly UserRelatedInfoService.UserRelatedInfoServiceClient _infoClient;

        public UserRelatedInfoController(UserRelatedInfoService.UserRelatedInfoServiceClient infoClient)
        {
            _infoClient = infoClient;
        }

        // GET: api/<CountryController>
        //get all countries
        [HttpGet("countries")]
        public async Task<IActionResult> GetCountries()
        {
            var reply = await _infoClient.GetAllCountriesAsync(new Protos.Common.Empty());
            return Ok(reply.Items);
        }

     
    }
}
