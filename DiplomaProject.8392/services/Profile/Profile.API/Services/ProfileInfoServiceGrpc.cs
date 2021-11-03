using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Profile.Domain.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Profile.API.Services
{
    public class ProfileInfoServiceGrpc: ProfileInfo.ProfileInfoBase
    {
        private readonly IProfileInfoService _service;
        private readonly IMapper _mapper;
        public ProfileInfoServiceGrpc(IProfileInfoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public override async Task<ProfileInfoResponse> GetProfileInfo(Request request, ServerCallContext context)
        {
            var user = await _service.GetProfileInfoAsync(request.Id);
            if(user==null)
            {
                return new ProfileInfoResponse
                {
                    NoUser = true
                };
            }
            var response = _mapper.Map<ProfileInfoResponse>(user);

            return response;
        }
      
    }
}
