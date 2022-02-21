using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Booking.Domain.Entities;
using Booking.Domain.Logic.Contracts;
using Booking.Domain.Logic.DTOs;
using Booking.Domain.Logic.IntegrationEvents.EventHandlers.Core;
using Booking.Domain.Logic.IntegrationEvents.Events;
using DAL.Base.Contracts;
using EventBus.Contracts;

namespace Booking.Domain.Logic.IntegrationEvents.EventHandlers
{
    //event handler that fires when user is created or updated
    public class UserCreatedOrUpdatedIntegrationEventHandler : UserBaseIntegrationEventHandler,
        IIntegrationEventHandler<UserCreatedOrUpdatedIntegrationEvent>
    {

        public UserCreatedOrUpdatedIntegrationEventHandler(
            IRepository<User> userRepository)
            :base(userRepository)
        {
        }

        public async Task Handle(UserCreatedOrUpdatedIntegrationEvent @event)
        {
          //if user with given id does not exist in the DB, create it
          if(!_userRepository.DoesItemWithIdExist(@event.UserId))
            {
                await _userRepository.CreateAsync(new User(@event.UserId));
            }
        }
    }
}
