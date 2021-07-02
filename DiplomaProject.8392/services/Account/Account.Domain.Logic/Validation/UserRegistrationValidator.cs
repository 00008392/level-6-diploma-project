using Account.Domain.Core;
using Account.Domain.Entities;
using Account.Domain.Logic.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Account.Domain.Logic.Validation
{
    public class UserRegistrationValidator : AbstractValidator<UserRegistrationDTO>
    {
        private readonly IRepository<User> _repository;
        public UserRegistrationValidator(IRepository<User> repository)
        {
            _repository = repository;
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is empty")
                .EmailAddress().WithMessage("Invalid email")
                .MustAsync(IsUnique).WithMessage("This email already exists");
            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is empty.")
                .MinimumLength(10).WithMessage("Length of password is minimum 10 symbols")
                .Must(ContainsDigit).WithMessage("Password shoud contain at least 1 digit")
                .Must(ContainsLowercase).WithMessage("Password should contain at least 1 lower case letter")
                .Must(ContainsUpperCase).WithMessage("Password should contain at least 1 upper case letter")
                .Must(ContainsSymbol).WithMessage("Password should contain at least 1 character");
            RuleFor(u => u.Role)
                .NotNull();

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

        private async Task<bool> IsUnique(string email, CancellationToken token)
        {
            var userWithEmail = (await _repository.GetItemsAsync(u => u.Email == email)).FirstOrDefault();
            if (userWithEmail == null)
            {
                return true;
            }
            return false;
        }
    }
}
