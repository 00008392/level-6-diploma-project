﻿using Account.Domain.Enums;
using Account.Domain.Logic.DTOs;
using Account.Domain.Logic.Interfaces;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account.API.Services
{
    public class LoginServiceGrpc: Login.LoginBase
    {
        private readonly ILoginService _service;
        public LoginServiceGrpc(ILoginService service)
        {
            _service = service;
        }

        public override async Task<LoginReply> GetLoggedUser(LoginRequest request, ServerCallContext context)
        {
            var loginDTO = new UserLoginDTO
            {
                Email = request.Email,
                Password = request.Password
            };
            var loggedUser = await _service.LoginUserAsync(loginDTO);
            if(loggedUser!=null)
            {
                var loginReply = new LoginReply
                {
                    Id = loggedUser.Id,
                    Email = loggedUser.Email,
                    Role = (int)loggedUser.Role
                };
                return loginReply;

            }
            return null;
        }
           
    }
}
