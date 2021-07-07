
using Account.Domain.Logic.DTOs;
using Account.Domain.Enums;
using Account.Domain.Logic.Interfaces;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Account.API.Services
{
    public class RegistrationServiceGrpc: Registration.RegistrationBase
    {
        private readonly IRegistrationService _service;
        public RegistrationServiceGrpc(IRegistrationService service)
        {
            _service = service;
        }

        public override async Task<RegistrationReply> RegisterUser(RegistrationRequest request, ServerCallContext context)
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
                return new RegistrationReply();
            }
            catch(ValidationException ex)
            {
                var metadata = new Metadata();
                var errorList = ex.Errors.ToList();
                foreach(var item in errorList)
                {
                    metadata.Add(item.PropertyName, item.ErrorMessage);
                }
                throw new RpcException(new Status(StatusCode.Internal, "Validation error"), metadata);
                
            }
           
            
          
        }
    }
}
