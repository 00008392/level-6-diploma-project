using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Contracts
{
    public interface IEventBus
    {
        void Publish(IntegrationEvent @event);
        void Subscribe<E, EH>()
            where E : IntegrationEvent
            where EH : IIntegrationEventHandler<E>;
        void Unsubscribe<E, EH>()
            where E : IntegrationEvent
            where EH : IIntegrationEventHandler<E>;
    }
}
