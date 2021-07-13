using FluentValidation;
using Profile.Domain.Core;
using Profile.Domain.Entities;
using Profile.Domain.Logic.DTOs;
using Profile.Domain.Logic.Helpers;
using Profile.Domain.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile.Domain.Logic.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IRepository<User> _repository;
        private readonly AbstractValidator<UpdateProfileDTO> _profileValidator;
        public ProfileService(IRepository<User> repository, 
            AbstractValidator<UpdateProfileDTO> profileValidator)
        {
            _repository = repository;
            _profileValidator = profileValidator;
        }
    

        public async Task DeleteProfile(long id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
            {
                throw new Exception("User does not exist");
            }
           await _repository.DeleteAsync(user);
        }

        public async Task UpdateProfile(UpdateProfileDTO profile)
        {
            var result = _profileValidator.Validate(profile);
            if(!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            var user = await _repository.GetByIdAsync(profile.Id);
            if(user==null)
            {
                throw new Exception("User does not exist");
            }
            user.FirstName = profile.FirstName;
            user.LastName = profile.LastName;
            user.Email = profile.Email;
            user.PhoneNumber = profile.PhoneNumber;
            user.DateOfBirth = profile.DateOfBirth;
            user.Gender = profile.Gender;
            user.Address = profile.Address;
            user.CityId = profile.CityId;
            user.UserInfo = profile.UserInfo;
           await _repository.UpdateAsync(user);
        }
    }
}
