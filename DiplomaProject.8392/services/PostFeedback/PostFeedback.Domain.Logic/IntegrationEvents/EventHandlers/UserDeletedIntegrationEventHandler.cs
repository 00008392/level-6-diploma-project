using PostFeedback.Domain.Logic.Contracts;
using PostFeedback.Domain.Logic.IntegrationEvents.EventHandlers.Core;
using PostFeedback.Domain.Logic.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBus.Contracts;
using AutoMapper;
using PostFeedback.Domain.Entities;
using DAL.Base.Contracts;

namespace PostFeedback.Domain.Logic.IntegrationEvents.EventHandlers
{
    //event handler that fires when user is deleted
    public class UserDeletedIntegrationEventHandler: UserBaseIntegrationEventHandler,
        IIntegrationEventHandler<UserDeletedIntegrationEvent>
    {
        public UserDeletedIntegrationEventHandler(IRepository<User> userRepository)
            :base(userRepository)
        {
        }
        //delete user
        public async Task Handle(UserDeletedIntegrationEvent @event)
        {
            //check if user exists in the database
            if(_userRepository.DoesItemWithIdExist(@event.UserId))
            {
                //if exists, delete user
                await _userRepository.DeleteAsync(@event.UserId);
            }
        }
    }
}
