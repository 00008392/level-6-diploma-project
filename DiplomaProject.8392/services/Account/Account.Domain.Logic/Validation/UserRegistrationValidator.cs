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
        public UserRegistrationValidator(IRepository<User> repository,
            AbstractValidator<PasswordBaseDTO> pwdValidator
            )
        {
            Include(pwdValidator);
            _repository = repository;
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is empty")
                .EmailAddress().WithMessage("Invalid email")
                .MustAsync(IsUnique).WithMessage("This email already exists");
           
            RuleFor(u => u.Role)
                .NotNull();

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
    }
}
