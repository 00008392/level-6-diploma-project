using FluentValidation;
using Profile.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile.Domain.Logic.Validation
{
    public class CreateProfileValidator: AbstractValidator<CreateProfileDTO>
    {
        public CreateProfileValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email cannot be empty");
            RuleFor(u => u.RegistrationDate)
                .NotNull().WithMessage("Registration date cannot be empty");
        }
    }
}
