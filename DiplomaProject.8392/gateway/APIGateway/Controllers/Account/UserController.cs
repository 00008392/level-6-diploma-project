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
using APIGateway.Authorization.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIGateway.Controllers.Account
{
    //controller for user manipulation
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //injecting grpc client to access services of account microservice
        private readonly UserService.UserServiceClient _userClient;
        //injecting authorization service for resource based authorization
        private readonly IAuthorizationService _authorizationService;
        public UserController(
            UserService.UserServiceClient userClient,
            IAuthorizationService authorizationService)
        {
            _userClient = userClient;
            _authorizationService = authorizationService;
        }

        // GET: api/<AccountController>
        //get all users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            //get users
            var reply = await _userClient.GetAllUsersAsync(new Protos.Common.Empty());
            //convert user data to proper dislay format
            reply.Items.ToList().ForEach(x => ConvertUserData(x));
            //return list of users
            return Ok(reply.Items);
        }

        // GET api/<AccountController>/5
        //get user details
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(long id)
        {
            //get user
            var reply = await _userClient.GetUserInfoAsync(new Request { Id = id});
            //if no user, return not found response
            if(reply.NoUser)
            {
                return NotFound("User not found");
            }
            //convert user data to proper dislay format (TimeStamp -> DateTime) and return it
            return Ok(ConvertUserData(reply));
        }
        // POST api/account
        //Register new user
        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            //convert user data to proper format (DateTime -> TimeStamp) and try to register
            var reply = await _userClient.RegisterUserAsync((RegisterRequest)ConvertUserData(request));
            //in case of errors, return bad request
            if(!reply.IsSuccess)
            {
                return BadRequest(reply);
            }
            //if successful, return created status code
            return StatusCode(201);
        }

        // PUT api/<AccountController>/5
        //Update user
        //Only authorized access
        [HttpPut("account")]
        public async Task<IActionResult> UpdateUser(UpdateRequest request)
        {
            //check if user is authorized to update information
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, request, "UserUpdatePolicy");
            if (!authorizationResult.Succeeded)
            {
                return Unauthorized();
            }
            //convert user data to proper format (DateTime -> TimeStamp) and try to update
            var reply = await _userClient.UpdateUserAsync((UpdateRequest)ConvertUserData(request));
            //in case of errors, return bad request
            if (!reply.IsSuccess)
            {
                return BadRequest(reply);
            }
            return NoContent();
        }
        // PUT api/<AccountController>/password/5
        //Update password
        //Only authorized access
        [HttpPut("account/password")]
        public async Task<IActionResult> UpdatePassword(ChangePasswordRequest request)
        {
            //check if user is authorized to update information
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, request, "UserUpdatePolicy");
            if (!authorizationResult.Succeeded)
            {
                return Unauthorized();
            }
            //try to change password
            var reply = await _userClient.ChangePasswordAsync(request);
            //in case of errors, return bad request
            if (!reply.IsSuccess)
            {
                return BadRequest(reply);
            }
            return NoContent();
        }

        // DELETE api/<AccountController>/5
        //Delete user
        //Only authorized access
        [Authorize]
        [HttpDelete("account")]
        public async Task<IActionResult> DeleteUser()
        {
            //get user id from claim 
            var id = AuthorizationHelper.GetLoggedUserId(User);
            //check if user is logged in
            if(id==null)
            {
                return Unauthorized();
            }
            //try to delete user with id from claim
            var reply = await _userClient.DeleteUserAsync(new Request { Id = (long)id });
            //in case of errors, return bad drequest
            if (!reply.IsSuccess)
            {
                return BadRequest(reply);
            }
            return NoContent();
        }

        private UserInfoResponse ConvertUserData(UserInfoResponse user)
        {
            //convert from TimeStamp to DateTime
            user.DateOfBirth = (DateTime)DateTimeConversion.FromTimeStampToDateTime(user.DateOfBirthTimeStamp);
            user.RegistrationDate = (DateTime)DateTimeConversion.FromTimeStampToDateTime(user.RegistrationDateTimeStamp);
            return user;
        }
        private IAccountRequest ConvertUserData(IAccountRequest user)
        {
            //convert from DateTime to TimeStamp  
            user.DateOfBirthTimeStamp = DateTimeConversion.FromDateTimeToTimeStamp(user.DateOfBirth);
            return user;
        }
    }
}
