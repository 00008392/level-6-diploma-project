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
        private readonly AbstractValidator<ChangePasswordDTO> _passwordValidator;
        private readonly IPasswordHandlingService _pwdService;
        private readonly AbstractValidator<UpdateProfileDTO> _profileValidator;
        public ProfileService(IRepository<User> repository, 
            AbstractValidator<ChangePasswordDTO> passwordValidator,
            AbstractValidator<UpdateProfileDTO> profileValidator)
        {
            _repository = repository;
            _passwordValidator = passwordValidator;
            _profileValidator = profileValidator;
        }
        public async Task ChangePassword(ChangePasswordDTO password)
        {
            var user = await _repository.GetByIdAsync(password.Id);
            if(user==null)
            {
                throw new Exception("User does not exist");

            }
            var result = await _passwordValidator.ValidateAsync(password);
            if(result.IsValid)
            {
                string salt = _pwdService.GetSalt();
                string hashedPassword = _pwdService.HashPassword(Convert.FromBase64String(salt), password.Password);
                user.PasswordSalt = salt;
                user.PasswordHash = hashedPassword;
              await  _repository.UpdateAsync(user);
            }
            throw new ValidationException(result.Errors);

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
