using FluentValidation;
using Profile.Domain.Entities;
using Profile.Domain.Logic.DTOs;
using Profile.Domain.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Profile.Domain.Logic.Exceptions;
using BaseClasses.Contracts;

namespace Profile.Domain.Logic.Services
{
    public class ProfileManipulationService : IProfileManipulationService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<City> _cityRepository;
        private readonly AbstractValidator<UpdateProfileDTO> _profileValidator;
        public ProfileManipulationService(IRepository<User> userRepository, IRepository<City> cityRepository,
            AbstractValidator<UpdateProfileDTO> profileValidator)
        {
            _userRepository = userRepository;
            _profileValidator = profileValidator;
            _cityRepository = cityRepository;
        }
    

        public async Task DeleteProfileAsync(long id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new ProfileNotFoundException(id);
            }
           await _userRepository.DeleteAsync(user);
        }


        public async Task UpdateProfileAsync(UpdateProfileDTO profile)
        {
            var user = await _userRepository.GetByIdAsync(profile.Id);
            if (user == null)
            {
                throw new ProfileNotFoundException(profile.Id);
            }
            var userWithEmail = (await _userRepository.GetFilteredAsync(u => u.Email == profile.Email && u.Id != profile.Id)).FirstOrDefault();
            if(userWithEmail!=null)
            {
                throw new UniqueConstraintViolationException(nameof(profile.Email), profile.Email);
            }
            var result = _profileValidator.Validate(profile);
            if(!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            if (profile.CityId != null)
            {
                if (!_cityRepository.DoesItemWithIdExist((long)profile.CityId))
                {
                    throw new ForeignKeyViolationException("city");
                }
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
             
            await _userRepository.UpdateAsync(user);

        }
    }
}
