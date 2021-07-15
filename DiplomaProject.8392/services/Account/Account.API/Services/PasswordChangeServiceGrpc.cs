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

        public override async Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest request, ServerCallContext context)
        {
            var passwordDTO = new ChangePasswordDTO
            {
                Id = request.Id,
                Password = request.Password
            };
            try
            {
                await _service.ChangePassword(passwordDTO);
                return new ChangePasswordResponse();
            }
            catch (ValidationException ex)
            {
                var metadata = new Metadata();
                var errorList = ex.Errors.ToList();
                foreach (var item in errorList)
                {
                    metadata.Add(item.PropertyName, item.ErrorMessage);
                }
                throw new RpcException(new Status(StatusCode.Internal, "Validation error"), metadata);
            }


        }
    }
}
