
using Account.Domain.Logic.Contracts;
using Account.Domain.Logic.DTOs;
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
        public RegistrationServiceGrpc(IRegistrationService service)
        {
            _service = service;
        }

        public override async Task<Response> RegisterUser(RegistrationRequest request, ServerCallContext context)
        {

            var userDTO = new UserRegistrationDTO
            {
                Email = request.Email,
                Password = request.Password,
                Role = (Domain.Enums.Role?)request.Role
            };
            var response = new Response();
            try
            {
                await _service.RegisterUserAsync(userDTO);
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
    }
}
