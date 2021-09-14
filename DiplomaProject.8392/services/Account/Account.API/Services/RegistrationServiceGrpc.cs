
using Account.Domain.Logic.Contracts;
using Account.Domain.Logic.DTOs;
using Account.Domain.Logic.IntegrationEvents.Events;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public RegistrationServiceGrpc(IRegistrationService service, IEventBus eventBus,
            IMapper mapper)
        {
            _service = service;
            _eventBus = eventBus;
            _mapper = mapper;
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
    }
}
