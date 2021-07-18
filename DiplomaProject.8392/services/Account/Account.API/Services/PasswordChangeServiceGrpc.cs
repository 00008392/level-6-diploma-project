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
    public class PasswordChangeServiceGrpc : PasswordChange.PasswordChangeBase
    {
        private readonly IPasswordChangeService _service;
        public PasswordChangeServiceGrpc(IPasswordChangeService service)
        {
            _service = service;
        }

        public override async Task<Response> ChangePassword(ChangePasswordRequest request, ServerCallContext context)
        {
            var passwordDTO = new ChangePasswordDTO
            {
                Id = request.Id,
                Password = request.Password
            };
            try
            {
                await _service.ChangePasswordAsync(passwordDTO);
                return new Response
                {
                    IsSuccess = true
                };
            }
            catch (ValidationException ex)
            {
                
                return ExceptionHandler.HandleValidationException(ex);
            }
            catch(Exception ex)
            {
                return ExceptionHandler.HandleException(ex);
            }
        }
    }
}
