using AutoMapper;
using Post.Domain.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.IntegrationEvents.EventHandlers.Core
{
    public abstract class BaseIntegrationEventHandler
    {
        protected readonly IEventHandlerService _service;
        protected readonly IMapper _mapper;
        protected BaseIntegrationEventHandler(IEventHandlerService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
    }
}
