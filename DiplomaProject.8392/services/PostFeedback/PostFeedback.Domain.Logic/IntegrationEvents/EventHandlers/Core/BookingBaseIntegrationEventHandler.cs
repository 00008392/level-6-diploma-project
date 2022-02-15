using BaseClasses.Contracts;
using PostFeedback.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.IntegrationEvents.EventHandlers.Core
{
    //base class for booking event handlers that has common dependencies
    public abstract class BookingBaseIntegrationEventHandler
    {
        protected readonly IRepository<Booking> _bookingRepository;

        protected BookingBaseIntegrationEventHandler(IRepository<Booking> bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
    }
}
