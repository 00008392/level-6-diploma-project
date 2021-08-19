using FluentValidation;
using Profile.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Profile.Domain.Entities;
using System.Threading;

namespace Profile.Domain.Logic.Validation
{
   public class ProfileValidator: AbstractValidator<UpdateProfileDTO>
    {
        public ProfileValidator()
        {
            RuleFor(u => u.FirstName)
                .NotEmpty().WithMessage("First name cannot be empty");
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email cannot be empty")
                .EmailAddress().WithMessage("Invalid email");
            RuleFor(u => u.DateOfBirth)
                .NotNull().WithMessage("Date of birth cannot be empty")
                .Must(IsAdult).WithMessage("You should be older than 18");
            RuleFor(u=>u.Gender)
                .NotNull().WithMessage("Gender cannot be empty");
        }

       
        private bool IsAdult(DateTime? dateOfBirth)
        {
            if(dateOfBirth == null)
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
