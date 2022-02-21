using Account.Domain.Entities;
using Account.PasswordHandling;
using AutoMapper;
using DAL.Base.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.Services.Core
{
    //class that contains common dependencies for all services except UserRelatedInfoService
    public abstract class BaseService
    {
        protected readonly IRepository<User> _repository;
        protected readonly IMapper _mapper;
        protected readonly IPasswordHandlingService _pwdService;

        protected BaseService(
            IRepository<User> repository,
            IMapper mapper,
            IPasswordHandlingService pwdService)
        {
            _repository = repository;
            _mapper = mapper;
            _pwdService = pwdService;
        }
      
    }
}
