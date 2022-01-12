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
        private readonly Login.LoginClient _loginClient;
        private readonly byte[] _key;

        public AuthenticationManager(Login.LoginClient loginClient, byte[] key)
        {
            _loginClient = loginClient;
            _key = key;
        }

        public async Task<LoginReply> AuthenticateAsync(LoginRequest request)
        {
            var reply = await _loginClient.GetLoggedUserAsync(request);
            if (reply.NoUser)
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                  
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
