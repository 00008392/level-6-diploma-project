using FluentValidation;
using Grpc.Core;
using Profile.Domain.Logic.DTOs;
using Profile.Domain.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Profile.API.ExceptionHandling;
using Microsoft.EntityFrameworkCore;

namespace Profile.API.Services
{
    public class ProfileManipulationServiceGrpc: ProfileManipulation.ProfileManipulationBase
    {
        private readonly IProfileManipulationService _service;
        public ProfileManipulationServiceGrpc(IProfileManipulationService service)
        {
            _service = service;
        }

        public override async Task<Response> UpdateProfile(UpdateRequest request, ServerCallContext context)
        {

            var updateDTO = new UpdateProfileDTO
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                DateOfBirth = request.DateOfBirth==null? null: request.DateOfBirth.ToDateTime(),
                Gender = (Domain.Enums.Gender)request.Gender,
                Address = request.Address,
                CityId = request.CityId,
                UserInfo = request.UserInfo
            };
            try
            {
                await _service.UpdateProfileAsync(updateDTO);
                return new Response {
                    IsSuccess = true
                };
            }
            catch(ValidationException ex)
            {
                return ExceptionHandler.HandleValidationException(ex);
            }
            catch(Exception ex)
            {
                return ExceptionHandler.HandleException(ex);
            }
        
        }
        public override async Task<Response> DeleteProfile(Request request, ServerCallContext context)
        {
            try
            {
                await _service.DeleteProfileAsync(request.Id);
                return new Response { 
                IsSuccess = true
                };
            }
            catch(Exception ex)
            {
                return ExceptionHandler.HandleException(ex);
            }

        }
    }
}
