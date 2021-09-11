using BaseClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
   public class BookingRequest: BaseEntity
    {
        public long GuestId { get; private set; }
        public User Guest { get; }
        public long AccommodationId { get;private set; }
        public Accommodation Accommodation { get; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        //To do: replace with enum (???)
        public bool IsAccepted { get; private set; }
        public bool IsCancelled { get; private set; }
        //Add enum by whom request was cancelled (???)

        public BookingRequest(long guestId, long accommodationId, 
            DateTime startDate, DateTime endDate)
        {
            GuestId = guestId;
            AccommodationId = accommodationId;
            StartDate = startDate;
            EndDate = endDate;
            IsAccepted = false;
            IsCancelled = false;
        }
        public void AcceptRequest()
        {
            IsAccepted = true;
        }
        //Add condition
        public void CancelRequest()
        {
            IsCancelled = true;
        }
        public void UpdateDates(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
