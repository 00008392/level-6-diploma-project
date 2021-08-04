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

        public override async Task<ProfileInfoResponse> GetProfileInfo(Request request, ServerCallContext context)
        {
            var user = await _service.GetProfileInfoAsync(request.Id);
            if(user==null)
            {
                return new ProfileInfoResponse {
                    NoUser = true
                };
            }
            var response = new ProfileInfoResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.DateOfBirth==null? null: Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)user.DateOfBirth, DateTimeKind.Utc)),
                RegistrationDate = Timestamp.FromDateTime(DateTime.SpecifyKind(user.RegistrationDate, DateTimeKind.Utc)),
                Gender = (int?)user.Gender,
                Address = user.Address,
                City = user.City == null? null: new City
                {
                    Id = user.City.Id,
                    Name = user.City.Name
                },
                UserInfo = user.UserInfo,
                ProfilePhoto = user.ProfilePhoto==null? null: Google.Protobuf.ByteString.CopyFrom(user.ProfilePhoto),
                MimeType = user.MimeType
            };

            return response;
        }
      
    }
}
