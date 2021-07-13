using Grpc.Core;
using Profile.Domain.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Profile.API.Services
{
    public class ProfileInfoServiceGrpc: ProfileInfo.ProfileInfoBase
    {
        private readonly IProfileInfoService _service;
        public ProfileInfoServiceGrpc(IProfileInfoService service)
        {
            _service = service;
        }

        public override Task<ProfileInfoResponse> GetProfileInfo(ProfileInfoRequest request, ServerCallContext context)
        {
            return Task.FromResult(new ProfileInfoResponse()
           );
        }
      
    }
}
