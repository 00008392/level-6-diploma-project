using EventBus.Contracts;
using EventBus.Events;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RabbitMQEventBus.EventBus
{
    public class RabbitMQEventBus : IEventBus
    {
        private readonly string _queueName;
        private readonly ISubscriptionManager _subscriptionManager;
        private readonly IServiceScopeFactory _scopeFactory;
        public RabbitMQEventBus(string queueName, ISubscriptionManager subscriptionManager,
            IServiceScopeFactory scopeFactory)
        {
            _queueName = queueName;
            _subscriptionManager = subscriptionManager;
            _scopeFactory = scopeFactory;
        }
        public void Publish(IntegrationEvent @event)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "event_bus", type: ExchangeType.Direct);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                var body = JsonSerializer.SerializeToUtf8Bytes(@event, @event.GetType(), new JsonSerializerOptions
                {
                    WriteIndented = true
                });
                channel.BasicPublish(
                       exchange: "event_bus",
                       routingKey: @event.GetType().Name,
                       mandatory: true,
                       basicProperties: properties,
                       body: body);
            }

        }

        public void Subscribe<E, EH>()
            where E : IntegrationEvent
            where EH : IIntegrationEventHandler<E>
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "event_bus",
                                  type: ExchangeType.Direct);

                channel.QueueDeclare(queue: _queueName,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                channel.QueueBind(queue: _queueName,
                                     exchange: "event_bus",
                                     routingKey: typeof(E).Name);

                var consumer = new AsyncEventingBasicConsumer(channel);

                consumer.Received += async (s, e) =>
                {
                    var eventName = e.RoutingKey;
                    var message = Encoding.UTF8.GetString(e.Body.Span);
                    var @event = JsonSerializer.Deserialize(message, typeof(E)) as E;

                    if (_subscriptionManager.HasSubscriptionsForEvent<E>())
                    {
                        using (var scope = _scopeFactory.CreateScope())
                        {
                            var subscriptions = _subscriptionManager.GetHandlersForEvent<E>();
                            foreach (var subscription in subscriptions)
                            {
                                var handler = scope.ServiceProvider.GetService(subscription) as IIntegrationEventHandler<E>;
                                await handler.Handle(@event);
                            }
                        }
                           
                    }
                };

                channel.BasicConsume(
                    queue: _queueName,
                    autoAck: false,
                    consumer: consumer);
            }

        }

        public void Unsubscribe<E, EH>()
            where E : IntegrationEvent
            where EH : IIntegrationEventHandler<E>
        {
            _subscriptionManager.RemoveSubscription<E, EH>();
        }
    }
}
