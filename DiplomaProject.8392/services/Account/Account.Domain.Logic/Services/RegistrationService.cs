using Account.Domain.Core;
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

namespace Account.Domain.Logic.Services
{
    public class RegistrationService : BaseService, IRegistrationService
    {
        private readonly AbstractValidator<UserRegistrationDTO> _validator;
        public RegistrationService(IRepository<User> repository, AbstractValidator<UserRegistrationDTO> validator, IPasswordHandlingService pwdService) 
            : base(repository, pwdService)
        {
            _validator = validator;
        }
        public async Task RegisterUserAsync(UserRegistrationDTO userDTO)
        {
           var result = await _validator.ValidateAsync(userDTO);
            if(result.IsValid)
            {
                var userWithEmail = (await _repository.GetFilteredAsync(u => u.Email == userDTO.Email)).FirstOrDefault();
                if (userWithEmail != null)
                {
                    throw new UniqueConstraintViolationException(nameof(userDTO.Email), userDTO.Email);
                }
                string salt = _pwdService.GetSalt();
                string hashedPassword = _pwdService.HashPassword(Convert.FromBase64String(salt) ,userDTO.Password);
                var user = new User
                {
                    Email = userDTO.Email,
                    RegistrationDate = DateTime.Now,
                    Role = userDTO.Role,
                    PasswordHash = hashedPassword,
                    PasswordSalt = salt
                };
                await _repository.CreateAsync(user);
            } else
            {
                throw new ValidationException(result.Errors);
            }

        }
    }
}
