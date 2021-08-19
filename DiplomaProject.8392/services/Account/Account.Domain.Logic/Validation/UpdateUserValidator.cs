using Account.Domain.Logic.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.Validation
{
    public class UpdateUserValidator: AbstractValidator<UpdateUserDTO>
    {
        public UpdateUserValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email cannot be empty");
        }
    }
}
