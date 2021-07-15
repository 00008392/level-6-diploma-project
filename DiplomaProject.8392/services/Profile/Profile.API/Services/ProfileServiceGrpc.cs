using FluentValidation;
using Grpc.Core;
using Profile.Domain.Logic.DTOs;
using Profile.Domain.Logic.Contracts;
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

        public override async Task<UpdateReply> UpdateProfile(UpdateRequest request, ServerCallContext context)
        {
            var updateDTO = new UpdateProfileDTO
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                DateOfBirth = request.DateOfBirth.ToDateTime(),
                Gender = (Domain.Enums.Gender)request.Gender,
                Address = request.Address,
                CityId = request.CityId,
                UserInfo = request.UserInfo
            };
            try
            {
                await _service.UpdateProfile(updateDTO);
                return new UpdateReply();
            }
            catch(ValidationException ex)
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
        public override async Task<DeleteReply> DeleteProfile(DeleteRequest request, ServerCallContext context)
        {
           await _service.DeleteProfile(request.Id);
            return new DeleteReply();
        }
    }
}
