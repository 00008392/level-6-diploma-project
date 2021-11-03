using Account.Domain.Entities;
using Account.Domain.Enums;
using Account.Domain.Logic.Contracts;
using Account.Domain.Logic.DTOs;
using Account.Domain.Logic.DTOs.Core;
using Account.Domain.Logic.Exceptions;
using Account.Domain.Logic.Services.Core;
using Account.PasswordHandling;
using AutoMapper;
using BaseClasses.Contracts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.Services
{
    public class UserManipulationService : BasePasswordService, IUserManipulationService
    {
        private readonly AbstractValidator<UserRegistrationDTO> _registrationValidator;
        private readonly AbstractValidator<UserBaseDTO> _baseValidator;
        private readonly IRepository<City> _cityRepository;
        private readonly AbstractValidator<IPasswordBaseDTO> _passwordValidator;
        public UserManipulationService(IRepositoryWithIncludes<User> repository, 
            AbstractValidator<UserRegistrationDTO> registrationValidator,
            IPasswordHandlingService pwdService, IRepository<City> cityRepository,
            AbstractValidator<UserBaseDTO> baseValidator, IMapper mapper,
             AbstractValidator<IPasswordBaseDTO> passwordValidator)
            : base(repository, pwdService, mapper)
        {
            _registrationValidator = registrationValidator;
            _cityRepository = cityRepository;
            _baseValidator = baseValidator;
            _passwordValidator = passwordValidator;
        }

        public async Task ChangePasswordAsync(ChangePasswordDTO passwordDTO)
        {
            var user = await FindUserAsync(passwordDTO.Id);
            var result = _passwordValidator.Validate(passwordDTO);
            if (result.IsValid)
            {
                var salt = _pwdService.GetSalt();
                var hashedPassword = _pwdService.HashPassword(Convert.FromBase64String(salt), passwordDTO.Password);
                user.SetPassword(hashedPassword, salt);
                await _repository.UpdateAsync(user);
            }
            else
            {
                throw new ValidationException(result.Errors);
            }

        }

        public async Task DeleteUserAsync(long id)
        {
            var user = await FindUserAsync(id);
            await _repository.DeleteAsync(user);
        }

        public async Task RegisterUserAsync(UserRegistrationDTO userDTO)
        {
            var result = _registrationValidator.Validate(userDTO);
            if (result.IsValid)
            {
                await CheckUserEmailAsync(u => u.Email == userDTO.Email, userDTO.Email);
                string salt = _pwdService.GetSalt();
                string hashedPassword = _pwdService.HashPassword(Convert.FromBase64String(salt), userDTO.Password);
                var user = _mapper.Map<User>(userDTO);
                user.SetPassword(hashedPassword, salt);
                await _repository.CreateAsync(user);
            }
            else
            {
                throw new ValidationException(result.Errors);
            }
        }

        public async Task UpdateUserAsync(UserUpdateDTO userDTO)
        {
            var user = await FindUserAsync(userDTO.Id);
            await CheckUserEmailAsync(u => u.Email == userDTO.Email && u.Id != userDTO.Id, userDTO.Email);
            var result = _baseValidator.Validate(userDTO);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            if (userDTO.CityId != null)
            {
                if (!_cityRepository.DoesItemWithIdExist((long)userDTO.CityId))
                {
                    throw new ForeignKeyViolationException("city");
                }
            }

            user.UpdateInfo(userDTO.FirstName, userDTO.LastName,
                userDTO.Email, userDTO.PhoneNumber, (DateTime)userDTO.DateOfBirth,
                (Gender)userDTO.Gender, userDTO.Address,
                userDTO.CityId, userDTO.UserInfo);
            await _repository.UpdateAsync(user);
        }
    }
}
