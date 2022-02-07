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
    //login grpc service
    public class LoginServiceGrpc : LoginService.LoginServiceBase
    {
        //inject service from domain logic layer
        private readonly ILoginService _service;
        private readonly IMapper _mapper;
        public LoginServiceGrpc(
            ILoginService service,
            IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public override async Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
        {
            //map request to dto
            var loginDTO = _mapper.Map<UserLoginDTO>(request);
            //try to log in
            var loggedUser = await _service.LoginUserAsync(loginDTO);
            if (loggedUser != null)
            {
                //if successful, map dto to response and return
                var loginReply = _mapper.Map<LoginResponse>(loggedUser);

                return loginReply;

            }
            //if not successful, notify that no user is returned
            return new LoginResponse
            {
                NoUser = true
            };

        }
    }
}
