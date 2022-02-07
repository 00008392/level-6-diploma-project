using Account.API;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace APIGateway.Authentication
{
    public class AuthenticationManager : IAuthenticationManager
    {
        //injecting grpc login client of account microservice
        private readonly LoginService.LoginServiceClient _loginClient;
        private readonly byte[] _key;

        public AuthenticationManager(LoginService.LoginServiceClient loginClient, byte[] key)
        {
            _loginClient = loginClient;
            _key = key;
        }
        //if authentication is successful, JWT token is returned
        public async Task<LoginResponse> AuthenticateAsync(LoginRequest request)
        {
            //try to log in
            var reply = await _loginClient.LoginAsync(request);
            if (reply.NoUser)
            {
                return null;
            }
            //if successful, generate token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                  //add email and id as claims
                    new Claim(ClaimTypes.Name, reply.Email),
                    new Claim(ClaimTypes.NameIdentifier, reply.Id.ToString())
                }),
                //token is valid within 1 hour
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(_key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            reply.JWTToken = tokenHandler.WriteToken(token);
            return reply;
        }
    }
}
