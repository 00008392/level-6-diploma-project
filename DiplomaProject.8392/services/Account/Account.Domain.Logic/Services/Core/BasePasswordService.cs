using Account.Domain.Entities;
using Account.PasswordHandling;
using BaseClasses.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.Services.Core
{
    public abstract class BasePasswordService: BaseService
    {
        protected readonly IPasswordHandlingService _pwdService;
        public BasePasswordService(IRepository<User> repository, IPasswordHandlingService pwdService)
            : base(repository)
        {
            _pwdService = pwdService;
        }

    }
}
