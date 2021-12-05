using BaseClasses.Entities;
using Booking.Domain.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.IntegrationEvents.EventHandlers.Core
{
    public abstract class BaseIntegrationEventHandler<T>
        where T: BaseEntity
    {
        protected readonly IEventHandlerService<T> _service;

        protected BaseIntegrationEventHandler(
            IEventHandlerService<T> service)
        {
            _service = service;
        }
    }
}
