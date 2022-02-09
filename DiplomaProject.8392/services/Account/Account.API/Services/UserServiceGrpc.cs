using Account.API.Helpers;
using Account.Domain.Logic.Contracts;
using Account.Domain.Logic.DTOs;
using Account.Domain.Logic.IntegrationEvents.Events;
using AutoMapper;
using EventBus.Contracts;
using API.ExceptionHandling;
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

        public override async Task<Response> ChangePassword(ChangePasswordRequest request,
            ServerCallContext context)
        {
            //map grpc request to dto
            var passwordDTO = _mapper.Map<ChangePasswordDTO>(request);
            //try to change password
            return await HandleActionAsync(passwordDTO, _service.ChangePasswordAsync);
        }

        public override async Task<Response> DeleteUser(Request request, ServerCallContext context)
        {
            var response = new Response();
            try
            {
                //try to delete user
                await _service.DeleteUserAsync(request.Id);
                //if successful, indicate it in response
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                //in case of error, indicate it in response
                response.HandleException(ex);
            }
            return response;
        }

        public override async Task<Response> RegisterUser(RegisterRequest request, ServerCallContext context)
        {
            //map grpc request to dto
            var userDTO = _mapper.Map<UserRegistrationDTO>(request);
            //try to register user
            return await HandleActionAsync(userDTO, _service.RegisterUserAsync);
        }

        public override async Task<Response> UpdateUser(UpdateRequest request, ServerCallContext context)
        {
            //map grpc request to dto
            var updateDTO = _mapper.Map<UserUpdateDTO>(request);
            //try to update user
            return await HandleActionAsync(updateDTO, _service.UpdateUserAsync);
        }
        public override async Task<UserInfoResponse> GetUserInfo(Request request,
            ServerCallContext context)
        {
            //get user with indicated id
            var user = await _service.GetUserInfoAsync(request.Id);
            if (user == null)
            {
                //if user does not exist, indicate it in response
                return new UserInfoResponse
                {
                    NoUser = true
                };
            }
            //if exists, map dto to grpc response
            var response = _mapper.Map<UserInfoResponse>(user);
            return response;
        }
        public override async Task<UserList> GetAllUsers(Empty request,
           ServerCallContext context)
        {
            //map list of users to grpc response
            return await GrpcServiceHelper.GetItems<UserList, UserInfoDTO, UserInfoResponse>
                (_service.GetAllUsersAsync, _mapper);
        }
        //method with common logic for user update, registration and password change
        private async Task<Response> HandleActionAsync<T>
            (T DTO, Func<T, Task> action) 
        {
            var response = new Response();
            try
            {
                //try to perform action
                await action(DTO);
                //if successful, indicate it in response
                response.IsSuccess = true;
            }
            catch (ValidationException ex)
            {
                //if validation fails, add validation errors to the response
                response.HandleValidationException(ex);
            }
            catch (Exception ex)
            {
                //in case of other error, indicate it in response
                response.HandleException(ex);
            }
            return response;
        }
    }
}
