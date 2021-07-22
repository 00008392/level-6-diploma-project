using Profile.Domain.Core;
using Profile.Domain.Entities;
using Profile.Domain.Logic.DTOs;
using Profile.Domain.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile.Domain.Logic.Services
{
    public class ProfileInfoService : IProfileInfoService
    {
        private readonly IRepository<User> _repository;
        public ProfileInfoService(IRepository<User> repository)
        {
            _repository = repository;
        }
        public async Task<ProfileInfoDTO> GetProfileInfo(long id)
        {
            var user = await _repository.GetByIdAsync(id, u=>u.City);
            if(user!=null)
            {
                var profileDTO = new ProfileInfoDTO
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    DateOfBirth = user.DateOfBirth,
                    RegistrationDate = user.RegistrationDate,
                    Gender = user.Gender,
                    Address = user.Address,
                    City = user.City,
                    UserInfo = user.UserInfo,
                    ProfilePhoto = user.ProfilePhoto,
                    MimeType = user.MimeType
                };
                return profileDTO;
            }

            return null;
        }
    }
}
