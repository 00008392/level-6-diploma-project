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
    public class PasswordChangeService : IPasswordChangeService
    {
        private readonly IRepository<User> _repository;
        private readonly AbstractValidator<ChangePasswordDTO> _passwordValidator;
        private readonly IPasswordHandlingService _pwdService;
        public PasswordChangeService(IRepository<User> repository,
           AbstractValidator<ChangePasswordDTO> passwordValidator,
           IPasswordHandlingService pwdService)
        {
            _repository = repository;
            _passwordValidator = passwordValidator;
            _pwdService = pwdService;
        }
        public async Task ChangePassword(ChangePasswordDTO password)
        {
            var user = await _repository.GetByIdAsync(password.Id);
            if (user == null)
            {
                throw new Exception("User does not exist");

            }
            var result = await _passwordValidator.ValidateAsync(password);
            if (result.IsValid)
            {
                string salt = _pwdService.GetSalt();
                string hashedPassword = _pwdService.HashPassword(Convert.FromBase64String(salt), password.Password);
                user.PasswordSalt = salt;
                user.PasswordHash = hashedPassword;
                await _repository.UpdateAsync(user);
            }
            throw new ValidationException(result.Errors);
        }
    }
}
