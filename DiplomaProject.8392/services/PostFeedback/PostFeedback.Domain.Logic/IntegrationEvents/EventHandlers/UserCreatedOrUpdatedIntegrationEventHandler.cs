using PostFeedback.Domain.Logic.Contracts;
using PostFeedback.Domain.Logic.DTOs;
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
    //event handler that fires when new user is registered or when user information is updated
    public class UserCreatedOrUpdatedIntegrationEventHandler: UserBaseIntegrationEventHandler,
        IIntegrationEventHandler<UserCreatedOrUpdatedIntegrationEvent>
    {
        private readonly IMapper _mapper;
        public UserCreatedOrUpdatedIntegrationEventHandler(
            IRepository<User> userRepository,
            IMapper mapper): base(
                userRepository)
        {
            _mapper = mapper;
        }
        //create/update user
        public async Task Handle(UserCreatedOrUpdatedIntegrationEvent @event)
        {
            //map event to user
            var userToHandle = _mapper.Map<User>(@event);
            //check if user with given id exists in the database
            //if exists, it means that user is being updated
            //if not, it means that new user is being created
            if (_userRepository.DoesItemWithIdExist(@event.UserId))
            {
               await _userRepository.UpdateAsync(userToHandle);
            } else
            {
                //create new user
                await _userRepository.CreateAsync(userToHandle);
            }
        }
        
    }
}
