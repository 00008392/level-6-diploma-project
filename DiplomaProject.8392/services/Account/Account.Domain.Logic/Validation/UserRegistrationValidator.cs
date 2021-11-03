
using Account.Domain.Entities;
using Account.Domain.Logic.DTOs;
using Account.Domain.Logic.DTOs.Core;
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
            AbstractValidator<IPasswordBaseDTO> pwdValidator, 
            AbstractValidator<UserBaseDTO> userValidator
            )
        {
            Include(pwdValidator);
            Include(userValidator);
            RuleFor(u => u.Role)
             .NotNull()
             .WithMessage("Role cannot be empty");
        }
       

    }
}
