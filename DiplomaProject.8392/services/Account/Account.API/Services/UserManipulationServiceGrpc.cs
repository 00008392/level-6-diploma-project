using Account.Domain.Logic.Contracts;
using Account.Domain.Logic.DTOs;
using Account.Domain.Logic.IntegrationEvents.Events;
using AutoMapper;
using EventBus.Contracts;
using ExceptionHandling;
using FluentValidation;
using Grpc.Core;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account.API.Services
{
    public class UserManipulationServiceGrpc: UserManipulation.UserManipulationBase
    {
        private readonly IUserManipulationService _service;
        private readonly IEventBus _eventBus;
        private readonly IMapper _mapper;
        public UserManipulationServiceGrpc(IUserManipulationService service, IEventBus eventBus,
            IMapper mapper)
        {
            _service = service;
            _eventBus = eventBus;
            _mapper = mapper;
        }

        public override async Task<Response> ChangePassword(ChangePasswordRequest request,
            ServerCallContext context)
        {
            var passwordDTO = _mapper.Map<ChangePasswordDTO>(request);
            var response = new Response();
            try
            {
                await _service.ChangePasswordAsync(passwordDTO);
                response.IsSuccess = true;
            }
            catch (ValidationException ex)
            {

                response.HandleValidationException(ex);
            }
            catch (Exception ex)
            {
                response.HandleException(ex);
            }
            return response;
        }

        public override async Task<Response> DeleteUser(Request request, ServerCallContext context)
        {
            var response = new Response();
            try
            {
                await _service.DeleteUserAsync(request.Id);
                response.IsSuccess = true;
                var integrationEvent = new UserDeletedIntegrationEvent(request.Id);
                _eventBus.Publish(integrationEvent);

            }
            catch (Exception ex)
            {
                response.HandleException(ex);
            }
            return response;
        }

        public override async Task<Response> RegisterUser(RegistrationRequest request, ServerCallContext context)
        {
            var userDTO = _mapper.Map<UserRegistrationDTO>(request);
            var response = new Response();
            try
            {
                await _service.RegisterUserAsync(userDTO);
                response.IsSuccess = true;
                var integrationEvent = _mapper.Map<UserCreatedIntegrationEvent>(userDTO);
                _eventBus.Publish(integrationEvent);
            }
            catch (ValidationException ex)
            {
                response.HandleValidationException(ex);
            }
            catch (Exception ex)
            {
                response.HandleException(ex);
            }
            return response;
        }

        public override async Task<Response> UpdateUser(UpdateRequest request, ServerCallContext context)
        {
            var updateDTO = _mapper.Map<UserUpdateDTO>(request);

            var response = new Response();
            try
            {
                await _service.UpdateUserAsync(updateDTO);
                response.IsSuccess = true;
                var integrationEvent = _mapper.Map<UserUpdatedIntegrationEvent>(updateDTO);

                _eventBus.Publish(integrationEvent);

            }
            catch (ValidationException ex)
            {
                response.HandleValidationException(ex);
            }
            catch (Exception ex)
            {
                response.HandleException(ex);
            }
            return response;
        }
    }
}
