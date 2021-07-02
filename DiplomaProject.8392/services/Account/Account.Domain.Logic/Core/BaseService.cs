using Account.Domain.Core;
using Account.Domain.Entities;
using Account.Domain.Logic.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.Core
{
    public abstract class BaseService
    {
        protected readonly IRepository<User> _repository;
        protected readonly IPasswordHandlingService _pwdService;
        public BaseService(IRepository<User> repository, IPasswordHandlingService pwdService)
        {
            _repository = repository;
            _pwdService = pwdService;
        }
    }
}
