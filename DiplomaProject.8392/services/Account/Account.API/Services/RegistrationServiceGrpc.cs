
using Account.Domain.Logic.Contracts;
using Account.Domain.Logic.DTOs;
using Account.Domain.Logic.IntegrationEvents.Events;
using EventBus.Contracts;
using ExceptionHandling;
using FluentValidation;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account.API.Services
{
    public class RegistrationServiceGrpc: Register.RegisterBase
    {
        private readonly IRegistrationService _service;
        private readonly IEventBus _eventBus;
        public RegistrationServiceGrpc(IRegistrationService service, IEventBus eventBus)
        {
            _service = service;
            _eventBus = eventBus;
        }

        public override async Task<Response> RegisterUser(RegistrationRequest request, ServerCallContext context)
        {

            var userDTO = new UserRegistrationDTO(request.Email, (Domain.Enums.Role?)request.Role,
                request.Password);
            var response = new Response();
            try
            {
                await _service.RegisterUserAsync(userDTO);
                response.IsSuccess = true;
                var integrationEvent = new UserCreatedIntegrationEvent(userDTO.Email, DateTime.Now);
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
