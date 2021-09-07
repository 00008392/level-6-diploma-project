using EventBus.Contracts;
using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.SubscriptionManager
{
    public class EventBusSubscriptionManager : ISubscriptionManager
    {
        private readonly Dictionary<string, 
            List<Type>> _handlers;
        public EventBusSubscriptionManager()
        {
            _handlers = new Dictionary<string,
                List<Type>>();
        }
        public void AddSubscription<E, EH>()
            where E : IntegrationEvent
            where EH : IIntegrationEventHandler<E>
        {
            if (!HasSubscriptionsForEvent<E>())
            {
                _handlers.Add(typeof(E).Name, new List<Type>());
            }
            if (!_handlers[typeof(E).Name].Any(handler => handler == typeof(EH)))
            {
                _handlers[typeof(E).Name].Add(typeof(EH));
            }
        }

        public IEnumerable<Type> GetHandlersForEvent<E>() where E : IntegrationEvent
        {
            return _handlers[typeof(E).Name];
        }

        public bool HasSubscriptionsForEvent<E>() where E : IntegrationEvent
        {
            return _handlers.ContainsKey(typeof(E).Name);
        }

        public void RemoveSubscription<E, EH>()
            where E : IntegrationEvent
            where EH : IIntegrationEventHandler<E>
        {
            var handlerToRemove = !HasSubscriptionsForEvent<E>() ? null :
                _handlers[typeof(E).Name].SingleOrDefault(handler => handler == typeof(EH));

            if (handlerToRemove != null)
            {
                _handlers[typeof(E).Name].Remove(handlerToRemove);
                if (!_handlers[typeof(E).Name].Any())
                {
                    _handlers.Remove(typeof(E).Name);
                  
                }

            }
        }
    }
}
