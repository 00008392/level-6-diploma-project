using FluentValidation;
using Profile.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile.Domain.Logic.Validation
{
    public class PasswordValidator: AbstractValidator<ChangePasswordDTO>
    {
        public PasswordValidator()
        {
            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is empty.")
                .MinimumLength(10).WithMessage("Length of password is minimum 10 symbols")
                .Must(ContainsDigit).WithMessage("Password shoud contain at least 1 digit")
                .Must(ContainsLowercase).WithMessage("Password should contain at least 1 lower case letter")
                .Must(ContainsUpperCase).WithMessage("Password should contain at least 1 upper case letter")
                .Must(ContainsSymbol).WithMessage("Password should contain at least 1 character");


        }

        private bool ContainsSymbol(string password)
        {
            return password.IndexOfAny("!@#$%^&*()".ToCharArray()) >= 0;
        }

        private bool ContainsUpperCase(string password)
        {
            return password.Any(c => char.IsUpper(c));
        }

        private bool ContainsLowercase(string password)
        {
            return password.Any(c => char.IsLower(c));
        }

        private bool ContainsDigit(string password)
        {
            return password.Any(c => char.IsDigit(c));
        }
    }
}
