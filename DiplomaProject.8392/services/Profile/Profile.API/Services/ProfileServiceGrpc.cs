using Grpc.Core;
using Profile.Domain.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Profile.API.Services
{
    public class ProfileServiceGrpc: Profile.ProfileBase
    {
        private readonly IProfileService _service;
        public ProfileServiceGrpc(IProfileService service)
        {
            _service = service;
        }

        public override Task<UpdateReply> UpdateProfile(UpdateRequest request, ServerCallContext context)
        {
            return Task.FromResult(new UpdateReply()
           );
        }
        public override Task<DeleteReply> DeleteProfile(DeleteRequest request, ServerCallContext context)
        {
            return Task.FromResult(new DeleteReply()
           );
        }
    }
}
