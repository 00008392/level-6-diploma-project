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
using BaseClasses.Contracts;
using PostFeedback.Domain.Entities;
using FluentValidation;
using BaseClasses.Exceptions;
using Domain.Helpers;

namespace PostFeedback.Domain.Logic.IntegrationEvents.EventHandlers
{
    //event handler that fires when new user is registered or when user information is updated
    public class UserCreatedOrUpdatedIntegrationEventHandler: UserBaseIntegrationEventHandler,
        IIntegrationEventHandler<UserCreatedOrUpdatedIntegrationEvent>
    {
        private readonly AbstractValidator<UserCreatedOrUpdatedIntegrationEvent> _validator;
        private readonly IMapper _mapper;
        public UserCreatedOrUpdatedIntegrationEventHandler(
            AbstractValidator<UserCreatedOrUpdatedIntegrationEvent> validator,
            IRepository<User> userRepository,
            IMapper mapper): base(
                userRepository)
        {
            _validator = validator;
            _mapper = mapper;
        }
        //create/update user
        public async Task Handle(UserCreatedOrUpdatedIntegrationEvent @event)
        {
            //check if user with given id exists in the database
            //if exists, it means that user is being updated
            //if not, it means that new user is being created
            var user = await _userRepository.GetByIdAsync(@event.UserId);
            if (user == null)
            {
                //create new user
                await HandleUserAsync(_userRepository.CreateAsync, @event);
            } else
            {
                //update user
                await HandleUserAsync(_userRepository.UpdateAsync, @event, @event.UserId);
            }
        }
        //method that either creates or updates user
        private async Task HandleUserAsync(
           Func<User, Task> repositoryAction,
           UserCreatedOrUpdatedIntegrationEvent @event,
           long? userId = null)
        {
            //validate user
            ServiceHelper.ValidateItem(_validator, @event);
            //ensure that email is unique
            if (userId != null)
            {
                //if user id is passed, it means that user is being updated
                //in this case email is checked for uniqueness with all emails except email of this user
                var userWithEmail = (await _userRepository.
               GetFilteredAsync(u => u.Email == @event.Email && u.Id != userId)).FirstOrDefault();
                if (userWithEmail != null)
                {
                    throw new UniqueConstraintViolationException("Email", @event.Email);
                }
            }
            //if user id is not passed, it means that new user is being created
            //in this case email is checked for uniqueness with all emails
            else
            {
                var userWithEmail = (await _userRepository
                .GetFilteredAsync(u => u.Email == @event.Email)).FirstOrDefault();
                if (userWithEmail != null)
                {
                    throw new UniqueConstraintViolationException("Email", @event.Email);
                }
            }
            //if all correct, map to domain entity and handle in the database
            var userToHandle = _mapper.Map<User>(@event);
            await repositoryAction(userToHandle);
        }
    }
}
