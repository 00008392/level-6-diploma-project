using Account.Domain.Logic.DTOs.Core;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.Validation.Core
{
    public class UserBaseValidator: AbstractValidator<UserBaseDTO>
    {
        //need this base for user update and user registration validation
        public UserBaseValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email cannot be empty")
                .EmailAddress().WithMessage("Invalid email");
            RuleFor(u => u.FirstName)
               .NotEmpty().WithMessage("First name cannot be empty");
            RuleFor(u => u.LastName)
               .NotEmpty().WithMessage("Last name cannot be empty");
            RuleFor(u => u.DateOfBirth)
               .NotNull().WithMessage("Date of birth cannot be empty")
               .Must(IsAdult).WithMessage("User should be older than 18");
            RuleFor(u => u.Gender)
                .NotNull().WithMessage("Gender cannot be empty");
        }
        private bool IsAdult(DateTime? dateOfBirth)
        {
            if (dateOfBirth == null)
            {
                return false;
            }
            if (((DateTime)dateOfBirth).AddYears(18) > DateTime.Now)
            {
                return false;
            }
            return true;
        }
    }
}
