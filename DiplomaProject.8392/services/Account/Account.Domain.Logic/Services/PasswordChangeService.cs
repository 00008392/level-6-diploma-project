
using Account.Domain.Entities;
using Account.Domain.Logic.Contracts;
using Account.Domain.Logic.Core;
using Account.Domain.Logic.DTOs;
using Account.Domain.Logic.Exceptions;
using Account.PasswordHandling;
using BaseClasses.Contracts;
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
        private readonly AbstractValidator<PasswordBaseDTO> _passwordValidator;
        public PasswordChangeService(IRepository<User> repository,
           AbstractValidator<PasswordBaseDTO> passwordValidator,
           IPasswordHandlingService pwdService):base(repository, pwdService)
        {
            _passwordValidator = passwordValidator;
        }
        public async Task ChangePasswordAsync(ChangePasswordDTO password)
        {
            var user = await _repository.GetByIdAsync(password.Id);
            if (user == null)
            {
                throw new AccountNotFoundException(password.Id);

            }
            var result = await _passwordValidator.ValidateAsync(password);
            if (result.IsValid)
            {
                var salt = _pwdService.GetSalt();
                var hashedPassword = _pwdService.HashPassword(Convert.FromBase64String(salt), password.Password);
                var userToUpdate = new User(user.Id, user.Email, user.RegistrationDate,
                    user.Role, hashedPassword, salt);
                await _repository.UpdateAsync(userToUpdate);
            } else
            {
                throw new ValidationException(result.Errors);
            }

        }
    }
}
