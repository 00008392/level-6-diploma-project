
using Account.Domain.Entities;
using Account.Domain.Logic.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Account.Domain.Logic.Validation
{
    public class UserRegistrationValidator : AbstractValidator<UserRegistrationDTO>
    {
        public UserRegistrationValidator(
            AbstractValidator<PasswordBaseDTO> pwdValidator
            )
        {
            Include(pwdValidator);
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is empty")
                .EmailAddress().WithMessage("Invalid email");
           
            RuleFor(u => u.Role)
                .NotNull()
                .WithMessage("Role cannot be empty");

        }

    }
}
