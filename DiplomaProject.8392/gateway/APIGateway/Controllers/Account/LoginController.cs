using Account.API;
using APIGateway.Authentication;
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
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationManager _authManager;

        public LoginController(IAuthenticationManager authManager)
        {
            _authManager = authManager;
        }

        // POST api/<LoginController>
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            //getting token based on credentials
            var user = await _authManager.AuthenticateAsync(request);
            //if token is null, it means that credentials are not correct
            if (user == null)
                return Unauthorized("Login failed");
            //if credentials are correct, user can use returned JWT to access authorized api endpoints
            return Ok(user);
        }

       
    }
}
