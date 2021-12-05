using Booking.Domain.Logic.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.Validation
{
    public class CreateUserValidator : AbstractValidator<CreateUserDTO>
    {
        public CreateUserValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("Email cannot be empty");
            RuleFor(p => p.FirstName)
               .NotEmpty().WithMessage("First name cannot be empty");
            RuleFor(p => p.LastName)
               .NotEmpty().WithMessage("Last name cannot be empty");
        }
    }
}
