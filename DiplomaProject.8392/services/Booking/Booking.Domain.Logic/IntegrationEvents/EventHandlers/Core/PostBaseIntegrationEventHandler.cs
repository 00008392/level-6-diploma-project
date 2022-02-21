using Booking.Domain.Entities;
using DAL.Base.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.IntegrationEvents.EventHandlers.Core
{
    //base class that contains common dependencies for post related event handlers 
    public abstract class PostBaseIntegrationEventHandler
    {
        protected readonly IRepository<Post> _postRepository;

        protected PostBaseIntegrationEventHandler(IRepository<Post> postRepository)
        {
            _postRepository = postRepository;
        }
    }
}
