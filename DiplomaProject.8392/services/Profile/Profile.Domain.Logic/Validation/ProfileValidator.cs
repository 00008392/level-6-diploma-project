using FluentValidation;
using Profile.Domain.Core;
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
    class ProfileValidator: AbstractValidator<UpdateProfileDTO>
    {
        private readonly IRepository<User> _repository;
        public ProfileValidator(IRepository<User> repository)
        {
            _repository = repository;
            RuleFor(u => u.FirstName)
                .NotEmpty().WithMessage("First name cannot be empty");
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is empty")
                .EmailAddress().WithMessage("Invalid email")
                .MustAsync(IsUnique).WithMessage("This email already exists");
            RuleFor(u => u.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth cannot be empty")
                .NotNull().WithMessage("Date of birth cannot be empty")
                .Must(IsAdult);
            RuleFor(u=>u.Gender)
                .NotEmpty().WithMessage("Date of birth cannot be empty")
                .NotNull().WithMessage("Gender cannot be empty");
            
        }

        private async Task<bool> IsUnique(string email, CancellationToken token)
        {
            var userWithEmail = (await _repository.GetFilteredAsync(u => u.Email == email)).FirstOrDefault();
            if (userWithEmail == null)
            {
                return true;
            }
            return false;
        }
        private bool IsAdult(DateTime dateOfBirth)
        {
            if (dateOfBirth.AddYears(18) > DateTime.Now)
            {
                return false;
            }
            return true;
        }
    }
}
