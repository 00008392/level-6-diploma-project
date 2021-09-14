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
using Profile.Domain.Logic.Services.Core;

namespace Profile.Domain.Logic.Services
{
    public class ProfileManipulationService :BaseService, IProfileManipulationService
    {
        private readonly IRepository<City> _cityRepository;
        private readonly AbstractValidator<UpdateProfileDTO> _profileValidator;
        public ProfileManipulationService(IRepository<User> userRepository, IRepository<City> cityRepository,
            AbstractValidator<UpdateProfileDTO> profileValidator):base(userRepository)
        {
            _profileValidator = profileValidator;
            _cityRepository = cityRepository;
        }
    

        public async Task DeleteProfileAsync(long id)
        {
            var user = await FindUserAsync(id);
           await _repository.DeleteAsync(user);
        }


        public async Task UpdateProfileAsync(UpdateProfileDTO profile)
        {
            var user = await FindUserAsync(profile.Id);
           await CheckUserEmailAsync(u => u.Email == profile.Email && u.Id != profile.Id, profile.Email);
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

            user.UpdateInfo(profile.FirstName, profile.LastName,
                profile.Email, profile.PhoneNumber, profile.DateOfBirth,
                profile.Gender, profile.Address, profile.CityId, profile.UserInfo);
            await _repository.UpdateAsync(user);

        }

        private async Task<User> FindUserAsync(long id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
            {
                throw new ProfileNotFoundException(id);
            }
            return user;
        }
    }
}
