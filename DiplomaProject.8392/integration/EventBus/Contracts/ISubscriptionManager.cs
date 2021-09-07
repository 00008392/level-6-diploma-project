using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Contracts
{
    public interface ISubscriptionManager
    {
        void AddSubscription<E, EH>()
            where E : IntegrationEvent
            where EH : IIntegrationEventHandler<E>;
        void RemoveSubscription<E, EH>()
            where E : IntegrationEvent
            where EH : IIntegrationEventHandler<E>;
        bool HasSubscriptionsForEvent<E>() 
            where E : IntegrationEvent;
        IEnumerable<Type> GetHandlersForEvent<E>()
            where E : IntegrationEvent;
    }
}
