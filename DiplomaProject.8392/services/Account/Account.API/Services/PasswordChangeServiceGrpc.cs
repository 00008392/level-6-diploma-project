
using Account.Domain.Logic.Contracts;
using Account.Domain.Logic.DTOs;
using AutoMapper;
using ExceptionHandling;
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
        private readonly IMapper _mapper;
        public PasswordChangeServiceGrpc(IPasswordChangeService service, IMapper mapper)
       
        {
            _service = service;
            _mapper = mapper;
        }

        public override async Task<Response> ChangePassword(ChangePasswordRequest request, ServerCallContext context)
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
            catch(Exception ex)
            {
                response.HandleException(ex);
            }
            return response;
        }
    }
}
