using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Contracts
{
    public interface IIntegrationEventHandler<T> where T: IntegrationEvent
    {
        Task Handle(T @event);
    }
}
