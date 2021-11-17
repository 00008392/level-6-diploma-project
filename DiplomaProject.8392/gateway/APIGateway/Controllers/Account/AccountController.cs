using Account.API;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIGateway.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        //methods in this controller return and take as parameters classes generated from proto definitions
        //but in some of them, custom classes from Models.Account namespace are used 
        //and then mapped with generated proto classes
        //this is done, because there is date format in these classes expressed as Google.Protobuf.TimeStamp type
        //but client should pass and receive properties of date format as DateTime, not TimeStamp 
        //therefore, custom models with date property as DateTime type were created
        private readonly UserManipulation.UserManipulationClient _manipulationClient;
        private readonly Login.LoginClient _loginClient;
        private readonly UserInfo.UserInfoClient _infoClient;
        private readonly IMapper _mapper;

        public AccountController(
            UserManipulation.UserManipulationClient manipulationClient,
            Login.LoginClient loginClient,
            UserInfo.UserInfoClient infoClient,
            IMapper mapper)
        {
            _manipulationClient = manipulationClient;
            _loginClient = loginClient;
            _infoClient = infoClient;
            _mapper = mapper;
        }

        // GET: api/<AccountController>
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var reply = await _infoClient.GetAllUsersAsync(new Empty());
            var users = _mapper.Map<ICollection<UserInfoResponse>,
                ICollection<Models.Account.UserInfoResponse>>(reply.Users);
            return Ok(users);
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(long id)
        {
            var reply = await _infoClient.GetUserInfoAsync(new Request { Id = id});
            if(reply.NoUser)
            {
                return NotFound("User not found");
            }
            var user = _mapper.Map<Models.Account.UserInfoResponse>(reply);
            return Ok(user);
        }
        //TO DO: move to separate controller and add JWT
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var reply = await _loginClient.GetLoggedUserAsync(request);
            if(reply.NoUser)
            {
                return Unauthorized();
            }
            return Ok(request.Email);
        }

        // POST api/account
        [HttpPost]
        public async Task<IActionResult> PostUser(Models.Account.RegistrationRequest request)
        {
            var user = _mapper.Map<RegistrationRequest>(request);
            var reply = await _manipulationClient.RegisterUserAsync(user);
            if(!reply.IsSuccess)
            {
                return BadRequest(reply);
            }
            return StatusCode(201);
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, Models.Account.UpdateRequest request)
        {
            if(id!=request.Id)
            {
                return BadRequest("Invalid user");
            }
            var user = _mapper.Map<UpdateRequest>(request);
            var reply = await _manipulationClient.UpdateUserAsync(user);
            if (!reply.IsSuccess)
            {
                return BadRequest(reply);
            }
            return NoContent();
        }
        // PUT api/<AccountController>/password/5
        [HttpPut("password/{id}")]
        public async Task<IActionResult> PutPassword(long id, [FromBody] string password)
        {
            var reply = await _manipulationClient.ChangePasswordAsync(new ChangePasswordRequest
            {
                Id = id,
                Password = password
            });
            if (!reply.IsSuccess)
            {
                return BadRequest(reply);
            }
            return NoContent();
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var reply = await _manipulationClient.DeleteUserAsync(new Request { Id = id });
            if (!reply.IsSuccess)
            {
                return NotFound(reply);
            }
            return NoContent();
        }
    }
}
