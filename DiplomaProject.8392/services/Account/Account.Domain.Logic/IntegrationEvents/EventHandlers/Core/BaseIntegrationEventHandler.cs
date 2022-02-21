using Account.Domain.Entities;
using AutoMapper;
using DAL.Base.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.IntegrationEvents.EventHandlers.Core
{
    //base class for event handlers that has common dependencies
    public abstract class BaseIntegrationEventHandler
    {
        protected readonly IRepository<Booking> _repository;
        protected BaseIntegrationEventHandler(
            IRepository<Booking> repository)
        {
            _repository = repository;
        }
    }
}
