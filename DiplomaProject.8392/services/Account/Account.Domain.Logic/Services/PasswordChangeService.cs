
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
    public class PasswordChangeService : BasePasswordService, IPasswordChangeService
    {
        private readonly AbstractValidator<PasswordBaseDTO> _passwordValidator;
        public PasswordChangeService(IRepository<User> repository,
           AbstractValidator<PasswordBaseDTO> passwordValidator,
           IPasswordHandlingService pwdService):base(repository, pwdService)
        {
            _passwordValidator = passwordValidator;
        }
        public async Task ChangePasswordAsync(ChangePasswordDTO passwordDTO)
        {
            var user = await FindUserAsync(passwordDTO.Id);
            var result =  _passwordValidator.Validate(passwordDTO);
            if (result.IsValid)
            {
                var salt = _pwdService.GetSalt();
                var hashedPassword = _pwdService.HashPassword(Convert.FromBase64String(salt), passwordDTO.Password);
                user.ChangePassword(hashedPassword, salt);
                await _repository.UpdateAsync(user);
            } else
            {
                throw new ValidationException(result.Errors);
            }

        }
    }
}
