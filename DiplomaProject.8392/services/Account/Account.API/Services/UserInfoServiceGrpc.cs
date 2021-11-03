using Account.Domain.Logic.Contracts;
using AutoMapper;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account.API.Services
{
    public class UserInfoServiceGrpc : UserInfo.UserInfoBase
    {
        private readonly IUserInfoService _service;
        private readonly IMapper _mapper;
        public UserInfoServiceGrpc(IUserInfoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        public override async Task<UserInfoResponse> GetUserInfo(Request request, 
            ServerCallContext context)
        {
            var user = await _service.GetProfileInfoAsync(request.Id);
            if (user == null)
            {
                return new UserInfoResponse
                {
                    NoUser = true
                };
            }
            var response = _mapper.Map<UserInfoResponse>(user);

            return response;
        }
    }
}
