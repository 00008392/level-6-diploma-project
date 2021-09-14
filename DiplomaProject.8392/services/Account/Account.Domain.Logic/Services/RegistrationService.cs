
using Account.Domain.Entities;
using Account.Domain.Logic.Core;
using Account.Domain.Logic.DTOs;
using Account.Domain.Logic.Contracts;
using Account.PasswordHandling;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Domain.Logic.Exceptions;
using Account.Domain.Enums;
using BaseClasses.Contracts;
using AutoMapper;

namespace Account.Domain.Logic.Services
{
    public class RegistrationService : BasePasswordService, IRegistrationService
    {
        private readonly AbstractValidator<UserRegistrationDTO> _validator;
        public RegistrationService(IRepository<User> repository, AbstractValidator<UserRegistrationDTO> validator,
            IPasswordHandlingService pwdService) 
            : base(repository, pwdService)
        {
            _validator = validator;
        }
        public async Task RegisterUserAsync(UserRegistrationDTO userDTO)
        {
           var result =  _validator.Validate(userDTO);
            if(result.IsValid)
            {
                await CheckUserEmailAsync(u => u.Email == userDTO.Email, userDTO.Email);
                string salt = _pwdService.GetSalt();
                string hashedPassword = _pwdService.HashPassword(Convert.FromBase64String(salt) ,userDTO.Password);
                var user = new User(userDTO.Email, DateTime.Now, (Role)userDTO.Role,
                    hashedPassword, salt);
                await _repository.CreateAsync(user);
            } else
            {
                throw new ValidationException(result.Errors);
            }

        }
    }
}
