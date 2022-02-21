using Booking.Domain.Entities;
using DAL.Base.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.IntegrationEvents.EventHandlers.Core
{
    //base class that contains common dependencies for user related event handlers 
    public abstract class UserBaseIntegrationEventHandler
    {
        protected readonly IRepository<User> _userRepository;

        protected UserBaseIntegrationEventHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
    }
}
