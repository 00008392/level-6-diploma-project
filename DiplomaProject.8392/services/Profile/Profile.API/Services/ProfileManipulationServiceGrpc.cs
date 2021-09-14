using FluentValidation;
using Grpc.Core;
using Profile.Domain.Logic.DTOs;
using Profile.Domain.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EventBus.Contracts;
using Profile.Domain.Logic.IntegrationEvents.Events;
using ExceptionHandling;
using AutoMapper;

namespace Profile.API.Services
{
    public class ProfileManipulationServiceGrpc: ProfileManipulation.ProfileManipulationBase
    {
        private readonly IProfileManipulationService _service;
        private readonly IEventBus _eventBus;
        private readonly IMapper _mapper;
        public ProfileManipulationServiceGrpc(IProfileManipulationService service, 
            IEventBus eventBus, IMapper mapper)
        {
            _service = service;
            _eventBus = eventBus;
            _mapper = mapper;
        }

        public override async Task<Response> UpdateProfile(UpdateRequest request, ServerCallContext context)
        {

            var updateDTO = _mapper.Map<UpdateProfileDTO>(request);
            
            var response = new Response();
            try
            {
                await _service.UpdateProfileAsync(updateDTO);
                response.IsSuccess = true;
                var integrationEvent = _mapper.Map<UserUpdatedIntegrationEvent>(updateDTO);
               
                _eventBus.Publish(integrationEvent);

            }
            catch(ValidationException ex)
            {
                response.HandleValidationException(ex);
            }
            catch(Exception ex)
            {
               response.HandleException(ex);
            }
            return response;
        
        }
        public override async Task<Response> DeleteProfile(Request request, ServerCallContext context)
        {
            var response = new Response();
            try
            {
                await _service.DeleteProfileAsync(request.Id);
                response.IsSuccess = true;
                var integrationEvent = new UserDeletedIntegrationEvent(request.Id);
                _eventBus.Publish(integrationEvent);

            }
            catch(Exception ex)
            {
                response.HandleException(ex);
            }
            return response;
        }
    }
}
