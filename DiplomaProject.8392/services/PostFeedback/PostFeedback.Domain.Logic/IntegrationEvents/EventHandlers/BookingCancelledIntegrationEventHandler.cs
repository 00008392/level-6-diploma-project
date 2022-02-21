using AutoMapper;
using EventBus.Contracts;
using PostFeedback.Domain.Entities;
using PostFeedback.Domain.Logic.Contracts;
using PostFeedback.Domain.Logic.IntegrationEvents.EventHandlers.Core;
using PostFeedback.Domain.Logic.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Base.Contracts;

namespace PostFeedback.Domain.Logic.IntegrationEvents.EventHandlers
{
    //event handler that fires when booking is cancelled
    public class BookingCancelledIntegrationEventHandler : BookingBaseIntegrationEventHandler,
        IIntegrationEventHandler<BookingCancelledIntegrationEvent>
    {
        public BookingCancelledIntegrationEventHandler(IRepository<Booking> bookingRepository)
            :base(bookingRepository)
        {
        }
        //if booking is cancelled, it can be deleted from post microservice because it needs 
        //only accepted bookings
        public async Task Handle(BookingCancelledIntegrationEvent @event)
        {
            //check if booking exists in the database
            if(_bookingRepository.DoesItemWithIdExist(@event.BookingId))
            {
                //if exists, delete booking
                await _bookingRepository.DeleteAsync(@event.BookingId);
            }
        }
    }
}
