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
        private readonly List<Type> _eventTypes;
        public EventBusSubscriptionManager()
        {
            _handlers = new Dictionary<string,
                List<Type>>();
            _eventTypes = new List<Type>();
        }
        public void AddSubscription<E, EH>()
            where E : IntegrationEvent
            where EH : IIntegrationEventHandler<E>
        {
            if (!_eventTypes.Contains(typeof(E)))
            {
                _eventTypes.Add(typeof(E));
            }
            if (!HasSubscriptionsForEvent<E>())
            {
                _handlers.Add(typeof(E).Name, new List<Type>());
            }
            if (!_handlers[typeof(E).Name].Any(handler => handler == typeof(EH)))
            {
                _handlers[typeof(E).Name].Add(typeof(EH));
            }
        }

        public IEnumerable<Type> GetHandlersForEvent(string eventName)
        {
            return _handlers[eventName];
        }

        public bool HasSubscriptionsForEvent<E>() where E : IntegrationEvent
        {
            return _handlers.ContainsKey(typeof(E).Name);
        }
        public bool HasSubscriptionsForEvent(string eventName) 
        {
            return _handlers.ContainsKey(eventName);
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
                    var eventType = _eventTypes.SingleOrDefault(e => e.Name == typeof(E).Name);
                    if (eventType != null)
                    {
                        _eventTypes.Remove(eventType);
                    }
                }

               
            }
        }
        public Type GetEventType(string eventName)
        {
           return _eventTypes.SingleOrDefault(t => t.Name == eventName);
        }
    }
}
