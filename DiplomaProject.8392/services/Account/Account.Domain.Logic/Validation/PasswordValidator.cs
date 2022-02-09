using Account.Domain.Entities;
using Account.Domain.Logic.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.Validation
{
    //validator for password
    public class PasswordValidator: AbstractValidator<IPasswordBaseDTO>
    {
        public PasswordValidator()
        {
            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is required")
                //password should be minimum 10 characters long
                .MinimumLength(10).WithMessage("Length of password should be minimum 10 symbols")
                //password should contain digits
                .Must(ContainsDigit).WithMessage("Password shoud contain at least 1 digit")
                //password should contain lower case and upper case letters
                .Must(ContainsLowercase).WithMessage("Password should contain at least 1 lower case letter")
                .Must(ContainsUpperCase).WithMessage("Password should contain at least 1 upper case letter")
                //password should contain special characters
                .Must(ContainsSymbol).WithMessage("Password should contain at least 1 character");
        }
        //check if password contains special characters
        private bool ContainsSymbol(string password)
        {
            return password!=null && password.IndexOfAny("!@#$%^&*()".ToCharArray()) >= 0;
        }
        //check if password contains upper case letters
        private bool ContainsUpperCase(string password)
        {
            return password!=null && password.Any(c => char.IsUpper(c));
        }
        //check if password contains lower case letters
        private bool ContainsLowercase(string password)
        {
            return password!=null && password.Any(c => char.IsLower(c));
        }
        //check if password contains digits
        private bool ContainsDigit(string password)
        {
            return password!=null && password.Any(c => char.IsDigit(c));
        }


    }
}
