using AutoMapper;
using PostFeedback.Domain.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.IntegrationEvents.EventHandlers.Core
{
    //base class for all event handlers that have common dependencies
    public abstract class BaseIntegrationEventHandler
    {
        //inject service responsible for event handling
        protected readonly IEventHandlerService _service;
        protected readonly IMapper _mapper;
        protected BaseIntegrationEventHandler(
            IEventHandlerService service,
            IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
    }
}
