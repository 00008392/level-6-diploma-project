using Account.API.Protos;
using Account.Domain.Logic.DTOs;
using Account.Domain.Enums;
using Account.Domain.Logic.Interfaces;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
           try
            {
                var userDTO = new UserRegistrationDTO
                {
                    Email = request.Email,
                    Password = request.Password,
                    Role = (Domain.Enums.Role)request.Role
                };
                await _service.RegisterUserAsync(userDTO);
                return new RegistrationReply
                {
                    IsSuccess = true
                };
            }
            catch(Exception ex)
            {
                return new RegistrationReply
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
