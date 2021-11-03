
using Account.Domain.Entities;
using Account.Domain.Logic.Exceptions;
using Account.PasswordHandling;
using AutoMapper;
using BaseClasses.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.Services.Core
{
    //need this base for all services
    public abstract class BaseService
    {
        protected readonly IRepositoryWithIncludes<User> _repository;
        protected readonly IMapper _mapper;
        protected BaseService(IRepositoryWithIncludes<User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        protected async Task<User> FindUserAsync(long id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
            {
                throw new UserNotFoundException(id);
            }
            return user;
        }
        protected async Task CheckUserEmailAsync(Expression<Func<User, bool>> filter, string email)
        {
            var userWithEmail = (await _repository.GetFilteredAsync(filter)).
                FirstOrDefault();
            if (userWithEmail != null)
            {
                throw new UniqueConstraintViolationException("Email", email);
            }
        }
    }
}
