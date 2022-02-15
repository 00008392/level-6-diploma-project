using Account.Domain.Logic.Contracts;
using Account.Domain.Logic.DTOs;
using Account.Domain.Logic.IntegrationEvents.Events;
using AutoMapper;
using EventBus.Contracts;
using Grpc.Helpers;
using FluentValidation;
using Grpc.Core;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account.API.Services
{
    //user CRUD grpc service
    public class UserServiceGrpc : UserService.UserServiceBase
    {
        //inject service from domain logic layer
        private readonly IUserService _service;
        private readonly IMapper _mapper;
        public UserServiceGrpc(
            IUserService service,
            IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        //update password
        public override async Task<Response> ChangePassword(ChangePasswordRequest request,
            ServerCallContext context)
        {
            //call helper method that handles create and update grpc actions
            return await GrpcServiceHelper.
                HandleCreateUpdateActionAsync<ChangePasswordDTO, Response, ChangePasswordRequest>
                (_service.ChangePasswordAsync, _mapper, request);
        }
        //delete account
        public override async Task<Response> DeleteUser(Request request, ServerCallContext context)
        {
            //call helper method that handles delete grpc action
            return await GrpcServiceHelper.HandleDeleteActionAsync<Response>
               (request.Id, _service.DeleteUserAsync);
        }
        //register account
        public override async Task<Response> RegisterUser(RegisterRequest request, ServerCallContext context)
        {
            //call helper method that handles create and update grpc actions
            return await GrpcServiceHelper.
                HandleCreateUpdateActionAsync<UserRegistrationDTO, Response, RegisterRequest>
                (_service.RegisterUserAsync, _mapper, request);
        }
        //update account information
        public override async Task<Response> UpdateUser(UpdateRequest request, ServerCallContext context)
        {
            //call helper method that handles create and update grpc actions
            return await GrpcServiceHelper.
               HandleCreateUpdateActionAsync<UserUpdateDTO, Response, UpdateRequest>
               (_service.UpdateUserAsync, _mapper, request);
        }
        //retrieve account information by id
        public override async Task<UserInfoResponse> GetUserInfo(Request request,
            ServerCallContext context)
        {
            //call helper method that handles retrieval by id grpc action
            return await GrpcServiceHelper.HandleRetrievalByIdAsync<UserInfoResponse, UserInfoDTO>
                 (request.Id, _service.GetUserInfoAsync, _mapper);
        }
        //retrieve list of users
        public override async Task<UserList> GetAllUsers(Empty request,
           ServerCallContext context)
        {
            //call helper method that handles retrieval of items and maps them to grpc response
            return await GrpcServiceHelper.GetItemsAsync<UserList, UserInfoDTO, UserInfoResponse>
                (_service.GetAllUsersAsync, _mapper);
        }
    }
}
