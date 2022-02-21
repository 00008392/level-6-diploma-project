using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Domain.Entities;
using Booking.Domain.Logic.Contracts;
using Booking.Domain.Logic.IntegrationEvents.EventHandlers.Core;
using Booking.Domain.Logic.IntegrationEvents.Events;
using DAL.Base.Contracts;
using EventBus.Contracts;

namespace Booking.Domain.Logic.IntegrationEvents.EventHandlers
{
    //event handler that fires when user is deleted
    public class UserDeletedIntegrationEventHandler : UserBaseIntegrationEventHandler,
        IIntegrationEventHandler<UserDeletedIntegrationEvent>
    {

        public UserDeletedIntegrationEventHandler(IRepository<User> userRepository)
            :base(userRepository)
        {
        }

        public async Task Handle(UserDeletedIntegrationEvent @event)
        {
            //ensure that user exists in the DB
            if(_userRepository.DoesItemWithIdExist(@event.UserId))
            {
                //delete user
                await _userRepository.DeleteAsync(@event.UserId);
            }
        }
    }
}
