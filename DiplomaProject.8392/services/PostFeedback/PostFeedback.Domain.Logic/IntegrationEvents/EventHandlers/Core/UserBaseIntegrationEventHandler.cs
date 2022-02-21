
using PostFeedback.Domain.Entities;
using PostFeedback.Domain.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Base.Contracts;

namespace PostFeedback.Domain.Logic.IntegrationEvents.EventHandlers.Core
{
    //base class for user create/update and delete event handlers that have common dependencies
    public abstract class UserBaseIntegrationEventHandler
    {
        protected readonly IRepository<User> _userRepository;

        protected UserBaseIntegrationEventHandler(
            IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

    }
}
