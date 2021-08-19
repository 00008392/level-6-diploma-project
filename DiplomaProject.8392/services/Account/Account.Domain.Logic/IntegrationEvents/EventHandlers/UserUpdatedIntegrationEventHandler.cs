using Account.Domain.Logic.Contracts;
using Account.Domain.Logic.DTOs;
using Account.Domain.Logic.IntegrationEvents.EventHandlers.Core;
using Account.Domain.Logic.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.IntegrationEvents.EventHandlers
{
   public class UserUpdatedIntegrationEventHandler: BaseIntegrationEventHandler
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
                Email = @event.Email
            };
            await _service.UpdateUserAsync(userDTO);
        }
    }
}
