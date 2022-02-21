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
    //event handler that fires when post is deleted
    public class PostDeletedIntegrationEventHandler : PostBaseIntegrationEventHandler,
        IIntegrationEventHandler<PostDeletedIntegrationEvent>
    {

        public PostDeletedIntegrationEventHandler(IRepository<Post> postRepository)
            :base(postRepository)
        {
        }
        public async Task Handle(PostDeletedIntegrationEvent @event)
        {
            //ensure that post exists in the DB
            if(_postRepository.DoesItemWithIdExist(@event.PostId))
            {
                //delete post
                await _postRepository.DeleteAsync(@event.PostId);
            }
        }
    }
}
