using Account.Domain.Entities;
using Account.PasswordHandling;
using AutoMapper;
using BaseClasses.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.Services.Core
{
    //need this base for login and user manipulation services
    public abstract class BasePasswordService: BaseService
    {
        protected readonly IPasswordHandlingService _pwdService;
        public BasePasswordService(IRepositoryWithIncludes<User> repository, 
            IPasswordHandlingService pwdService, IMapper mapper)
            : base(repository, mapper)
        {
            _pwdService = pwdService;
        }

    }
}
