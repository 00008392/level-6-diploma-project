using Account.Domain.Logic.Contracts;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.IntegrationEvents.EventHandlers.Core
{
    public abstract class BaseIntegrationEventHandler
    {
        protected readonly IEventHandlerService _service;

        protected BaseIntegrationEventHandler(IEventHandlerService service)
        {
            _service = service;
        }
    }
}
