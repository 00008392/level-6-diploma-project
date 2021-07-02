using Account.Domain.Core;
using Account.Domain.Entities;
using Account.Domain.Logic.Core;
using Account.Domain.Logic.DTOs;
using Account.Domain.Logic.Helpers;
using Account.Domain.Logic.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        //can return exception
        public async Task RegisterUserAsync(UserRegistrationDTO userDTO)
        {
            await _validator.ValidateAndThrowAsync(userDTO);
            string hashedPassword = _pwdService.CreatePasswordHash(userDTO.Password);
            string salt = _pwdService.GetSalt();


            var user = new User
            {
                Email = userDTO.Email,
                RegistrationDate = DateTime.Now,
                Role = userDTO.Role,
                PasswordHash = hashedPassword,
                PasswordSalt = salt
            };
            await _repository.CreateAsync(user);
        }
    }
}
