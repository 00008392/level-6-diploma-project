using AutoMapper;
using Booking.Domain.Entities;
using Booking.Domain.Logic.Contracts;
using Booking.Domain.Logic.DTOs;
using Booking.Domain.Logic.IntegrationEvents.EventHandlers.Core;
using Booking.Domain.Logic.IntegrationEvents.Events;
using DAL.Base.Contracts;
using EventBus.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.IntegrationEvents.EventHandlers
{
    //event handler that fires when post is created or updated
    public class PostCreatedOrUpdatedIntegrationEventHandler : PostBaseIntegrationEventHandler,
        IIntegrationEventHandler<PostCreatedOrUpdatedIntegrationEvent>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<User> _userRepository;

        public PostCreatedOrUpdatedIntegrationEventHandler(
            IMapper mapper,
            IRepository<Post> postRepository,
            IRepository<User> userRepository)
            :base(postRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task Handle(PostCreatedOrUpdatedIntegrationEvent @event)
        {
            //map event to domain entity
            var post = _mapper.Map<Post>(@event);
            //ensure that post owner exists in the database
            if(_userRepository.DoesItemWithIdExist(@event.OwnerId))
            {
                //if post exists in DB, it is being updated
                if(_postRepository.DoesItemWithIdExist(@event.PostId))
                {
                    //update post
                   await _postRepository.UpdateAsync(post);
                }
                //if post does not exist in the DB, it is being created
                else
                {
                    //create new post
                    await _postRepository.CreateAsync(post);
                }
            }
        }
    }
}
