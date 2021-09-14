using Account.Domain.Logic.Contracts;
using Account.Domain.Logic.DTOs;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public LoginServiceGrpc(ILoginService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public override async Task<LoginReply> GetLoggedUser(LoginRequest request, ServerCallContext context)
        {
            var loginDTO = _mapper.Map<UserLoginDTO>(request);
            var loggedUser = await _service.LoginUserAsync(loginDTO);
            if (loggedUser != null)
            {
                var loginReply = _mapper.Map<LoginReply>(loggedUser);
                
                return loginReply;

            }

            return new LoginReply(noUser: true);
           
        }
    }
}
