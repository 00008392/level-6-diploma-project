using Account.API;
using APIGateway.Helpers;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIGateway.Controllers.Account
{
    [Route("api/users")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManipulation.UserManipulationClient _manipulationClient;
        private readonly UserInfo.UserInfoClient _infoClient;

        public AccountController(
            UserManipulation.UserManipulationClient manipulationClient,
            UserInfo.UserInfoClient infoClient)
        {
            _manipulationClient = manipulationClient;
            _infoClient = infoClient;
        }

        // GET: api/<AccountController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var reply = await _infoClient.GetAllUsersAsync(new Protos.Common.Empty());
            reply.Items.ToList().ForEach(x => ConvertUserData(x));
            return Ok(reply.Items);
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var reply = await _infoClient.GetUserInfoAsync(new Request { Id = id});
            
            if(reply.NoUser)
            {
                return NotFound("User not found");
            }
            return Ok(ConvertUserData(reply));
        }
        // POST api/account
        [HttpPost]
        public async Task<IActionResult> Post(RegistrationRequest request)
        {
            var reply = await _manipulationClient.RegisterUserAsync((RegistrationRequest)ConvertUserData(request));
            if(!reply.IsSuccess)
            {
                return BadRequest(reply);
            }
            return StatusCode(201);
        }

        // PUT api/<AccountController>/5
        [Authorize]
        [HttpPut("account")]
        public async Task<IActionResult> Put(UpdateRequest request)
        {
            if(GetLoggedUserId()!= request.Id)
            {
                return BadRequest("Invalid user");
            }
            //request.DateOfBirthTimeStamp = DateTimeConversion.FromDateTimeToTimeStamp(request.DateOfBirth);
            var reply = await _manipulationClient.UpdateUserAsync((UpdateRequest)ConvertUserData(request));
            if (!reply.IsSuccess)
            {
                return BadRequest(reply);
            }
            return NoContent();
        }
        // PUT api/<AccountController>/password/5
        [Authorize]
        [HttpPut("account/password")]
        public async Task<IActionResult> Put(ChangePasswordRequest request)
        {
            if (GetLoggedUserId()!= request.Id)
            {
                return Unauthorized();
            }
            var reply = await _manipulationClient.ChangePasswordAsync(request);
            if (!reply.IsSuccess)
            {
                return BadRequest(reply);
            }
            return NoContent();
        }

        // DELETE api/<AccountController>/5
        [Authorize]
        [HttpDelete("account")]
        public async Task<IActionResult> Delete()
        {
            var id = GetLoggedUserId();
            var reply = await _manipulationClient.DeleteUserAsync(new Request { Id = id });
            if (!reply.IsSuccess)
            {
                return NotFound(reply);
            }
            return NoContent();
        }

        private UserInfoResponse ConvertUserData(UserInfoResponse user)
        {
            user.DateOfBirth = (DateTime)DateTimeConversion.FromTimeStampToDateTime(user.DateOfBirthTimeStamp);
            user.RegistrationDate = (DateTime)DateTimeConversion.FromTimeStampToDateTime(user.RegistrationDateTimeStamp);
            return user;
        }
        private IAccountRequest ConvertUserData(IAccountRequest user)
        {
            user.DateOfBirthTimeStamp = DateTimeConversion.FromDateTimeToTimeStamp(user.DateOfBirth);
            return user;
        }
        private int GetLoggedUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}
