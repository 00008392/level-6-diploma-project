using Account.API.ExceptionHandling;
using Account.Domain.Logic.Contracts;
using Account.Domain.Logic.DTOs;
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
                Role = (Domain.Enums.Role)request.Role
            };
            try
            {
                await _service.RegisterUserAsync(userDTO);
                return new Response { 
                IsSuccess = true
                };
            }
            catch (ValidationException ex)
            {
                return ExceptionHandler.HandleValidationException(ex);
            }
            catch (Exception ex)
            {
                return ExceptionHandler.HandleException(ex);
            }


        }
    }
}
