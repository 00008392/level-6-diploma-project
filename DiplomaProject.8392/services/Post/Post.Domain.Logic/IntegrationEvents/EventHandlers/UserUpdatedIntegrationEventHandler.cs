using Post.Domain.Logic.Contracts;
using Post.Domain.Logic.DTOs;
using Post.Domain.Logic.IntegrationEvents.EventHandlers.Core;
using Post.Domain.Logic.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBus.Contracts;

namespace Post.Domain.Logic.IntegrationEvents.EventHandlers
{
    public class UserUpdatedIntegrationEventHandler: BaseIntegrationEventHandler, IIntegrationEventHandler<UserUpdatedIntegrationEvent>
    {
        public UserUpdatedIntegrationEventHandler(IEventHandlerService service)
            :base(service)
        {

        }
        public async Task Handle(UserUpdatedIntegrationEvent @event)
        {
            var userDTO = new UpdateUserDTO
            {
                Id = @event.UserId,
                Email = @event.Email,
                FirstName = @event.FirstName,
                LastName = @event.LastName,
                PhoneNumber = @event.PhoneNumber
            };
            await _service.UpdateUserAsync(userDTO);
        }

    }
}
