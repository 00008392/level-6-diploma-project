
using Profile.Domain.Entities;
using Profile.Domain.Logic.DTOs;
using Profile.Domain.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseClasses.Contracts;

namespace Profile.Domain.Logic.Services
{
    public class ProfileInfoService : IProfileInfoService
    {
        private readonly IRepository<User> _repository;
        public ProfileInfoService(IRepository<User> repository)
        {
            _repository = repository;
        }
        public async Task<ProfileInfoDTO> GetProfileInfoAsync(long id)
        {
            var user = await _repository.GetByIdAsync(id, u=>u.City);
            if(user!=null)
            {
                var profileDTO = new ProfileInfoDTO(user.Id, user.FirstName, user.LastName,
                    user.Email, user.PhoneNumber, user.DateOfBirth, user.Gender,
                    user.Address, user.UserInfo, user.RegistrationDate, user.City,
                    user.ProfilePhoto, user.MimeType);
               
                return profileDTO;
            }

            return null;
        }
    }
}
