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

namespace Profile.API.Services
{
    public class ProfileManipulationServiceGrpc: ProfileManipulation.ProfileManipulationBase
    {
        private readonly IProfileManipulationService _service;
        private readonly IEventBus _eventBus;
        public ProfileManipulationServiceGrpc(IProfileManipulationService service, IEventBus eventBus)
        {
            _service = service;
            _eventBus = eventBus;
        }

        public override async Task<Response> UpdateProfile(UpdateRequest request, ServerCallContext context)
        {

            var updateDTO = new UpdateProfileDTO(request.Id,
                request.FirstName, request.LastName,
                request.Email, request.PhoneNumber, request.DateOfBirth?.ToDateTime(),
                (Domain.Enums.Gender?)request.Gender, request.Address, request.UserInfo, 
                request.CityId);
            
            var response = new Response();
            try
            {
                await _service.UpdateProfileAsync(updateDTO);
                response.IsSuccess = true;
                var integrationEvent = new UserUpdatedIntegrationEvent(updateDTO.Id,
                    updateDTO.FirstName,
                    updateDTO.LastName,
                    updateDTO.Email,
                    updateDTO.PhoneNumber,
                    updateDTO.DateOfBirth,
                    updateDTO.Gender,
                    updateDTO.Address,
                    updateDTO.UserInfo,
                    updateDTO.CityId);
               
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
