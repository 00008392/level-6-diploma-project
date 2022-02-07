using Account.Domain.Logic.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.Validation
{
    //validaiton of base user properties, common for registration and update
    public class UserValidator: AbstractValidator<UserBaseDTO>
    {
        //need this base for user update and user registration validation
        public UserValidator()
        {
            //emai is required and should be valid email address
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email");
            //first name is required
            RuleFor(u => u.FirstName)
               .NotEmpty().WithMessage("First name is required");
            //last name is required
            RuleFor(u => u.LastName)
               .NotEmpty().WithMessage("Last name is required");
            //date of birth is required and user should ne older than 18
            RuleFor(u => u.DateOfBirth)
               .NotEmpty().WithMessage("Date of birth is required")
               .Must(IsAdult).WithMessage("User should be older than 18");
            //gender is required
            RuleFor(u => u.Gender)
                .NotEmpty().WithMessage("Gender is required");
            //country is required
            RuleFor(u => u.CountryId)
               .NotEmpty().WithMessage("Country is required");
        }
        //check if user is older than 18
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
