using Account.Domain.Core;
using Account.Domain.Entities;
using Account.Domain.Logic.Contracts;
using Account.Domain.Logic.Core;
using Account.Domain.Logic.DTOs;
using Account.PasswordHandling;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.Services
{
    public class PasswordChangeService : BaseService, IPasswordChangeService
    {
        private readonly AbstractValidator<ChangePasswordDTO> _passwordValidator;
        public PasswordChangeService(IRepository<User> repository,
           AbstractValidator<ChangePasswordDTO> passwordValidator,
           IPasswordHandlingService pwdService):base(repository, pwdService)
        {
            _passwordValidator = passwordValidator;
        }
        public async Task ChangePasswordAsync(ChangePasswordDTO password)
        {
            var user = await _repository.GetByIdAsync(password.Id);
            if (user == null)
            {
                throw new Exception("User does not exist");

            }
            var result = await _passwordValidator.ValidateAsync(password);
            if (result.IsValid)
            {
                var salt = _pwdService.GetSalt();
                var hashedPassword = _pwdService.HashPassword(Convert.FromBase64String(salt), password.Password);
                user.PasswordSalt = salt;
                user.PasswordHash = hashedPassword;
                await _repository.UpdateAsync(user);
            }
            throw new ValidationException(result.Errors);
        }
    }
}
