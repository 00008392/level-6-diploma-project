using Grpc.Core;
using Profile.Domain.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Profile.API.Services
{
    public class PasswordChangeServiceGrpc: PasswordChange.PasswordChangeBase
    {
        private readonly IPasswordChangeService _service;
        public PasswordChangeServiceGrpc(IPasswordChangeService service)
        {
            _service = service;
        }

        public override Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest request, ServerCallContext context)
        {
            return Task.FromResult(new ChangePasswordResponse()
           );
        }
    }
}
