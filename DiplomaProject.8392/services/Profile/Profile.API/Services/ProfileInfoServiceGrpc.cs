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
        public ProfileInfoServiceGrpc(IProfileInfoService service)
        {
            _service = service;
        }

        public override async Task<ProfileInfoResponse> GetProfileInfo(ProfileInfoRequest request, ServerCallContext context)
        {
            var user = await _service.GetProfileInfo(request.Id);
            if(user==null)
            {
                return new ProfileInfoResponse();
            }
            var response = new ProfileInfoResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = Timestamp.FromDateTime((DateTime)user.DateOfBirth),
                RegistrationDate = Timestamp.FromDateTime(user.RegistrationDate),
                Gender = (int)user.Gender,
                Address = user.Address,
                City = new City
                {
                    Id = user.City.Id,
                    Name = user.City.Name
                },
                UserInfo = user.UserInfo,
                ProfilePhoto = Google.Protobuf.ByteString.CopyFrom(user.ProfilePhoto),
                MimeType = user.MimeType
            };

            return response;
        }
      
    }
}
