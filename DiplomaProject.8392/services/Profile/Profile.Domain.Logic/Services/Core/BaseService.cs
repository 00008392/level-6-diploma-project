using BaseClasses.Contracts;
using Profile.Domain.Entities;
using Profile.Domain.Logic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Profile.Domain.Logic.Services.Core
{
    public abstract class BaseService
    {
        protected readonly IRepositoryWithIncludes<User> _repository;

        protected BaseService(IRepositoryWithIncludes<User> repository)
        {
            _repository = repository;
        }
        protected async Task CheckUserEmailAsync(Expression<Func<User, bool>> filter,
            string email)
        {
            var userWithEmail = (await _repository.GetFilteredAsync(filter)).FirstOrDefault();
            if (userWithEmail != null)
            {
                throw new UniqueConstraintViolationException("Email", email);
            }
        }
    }
}
