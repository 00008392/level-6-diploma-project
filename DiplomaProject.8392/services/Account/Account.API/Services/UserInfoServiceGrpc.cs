using Account.Domain.Logic.Contracts;
using Account.Domain.Logic.DTOs;
using AutoMapper;
using Grpc.Core;
using Protos.Common;
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
        public override async Task<UserList> GetAllUsers(Empty request,
           ServerCallContext context)
        {
            var users = await _service.GetAllUsersAsync();
            var usersList = _mapper.Map<ICollection<UserInfoDTO>, ICollection<UserInfoResponse>>(users);
            var response = new UserList();
            response.Users.Add(usersList);
            return response;
        }
    }
}
