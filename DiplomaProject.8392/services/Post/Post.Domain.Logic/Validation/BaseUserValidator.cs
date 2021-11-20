using FluentValidation;
using Post.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.Validation
{
    public class BaseUserValidator: AbstractValidator<CreateUserDTO>
    {
        public BaseUserValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email cannot be empty");
            RuleFor(u => u.FirstName)
                .NotEmpty().WithMessage("First name cannot be empty");
            RuleFor(u => u.LastName)
                .NotEmpty().WithMessage("Last name cannot be empty");
        }
    }
}
