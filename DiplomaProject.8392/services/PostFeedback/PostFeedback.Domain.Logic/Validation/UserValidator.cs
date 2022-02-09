using FluentValidation;
using PostFeedback.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Validation
{
    //validation for user creation and modification
    public class UserValidator: AbstractValidator<UserDTO>
    {
        public UserValidator()
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
