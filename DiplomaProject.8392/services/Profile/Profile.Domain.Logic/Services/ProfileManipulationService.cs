using FluentValidation;
using Profile.Domain.Core;
using Profile.Domain.Entities;
using Profile.Domain.Logic.DTOs;
using Profile.Domain.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Profile.Domain.Logic.Exceptions;

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
    

        public async Task DeleteProfile(long id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new ProfileNotFoundException(id);
            }
           await _userRepository.DeleteAsync(user);
        }


        public async Task UpdateProfile(UpdateProfileDTO profile)
        {
            if (!_userRepository.IfExists(profile.Id))
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

            var user = new User
            {
                Id = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Email = profile.Email,
                PhoneNumber = profile.PhoneNumber,
                DateOfBirth = profile.DateOfBirth,
                Gender = profile.Gender,
                Address = profile.Address,
                CityId = profile.CityId,
                UserInfo = profile.UserInfo
            };
              if(user.CityId!=null)
            {
                if(!_cityRepository.IfExists((long)user.CityId))
                {
                    throw new ForeignKeyViolationException("city");
                }
            }
                await _userRepository.UpdateAsync(user);

          
        }
    }
}
