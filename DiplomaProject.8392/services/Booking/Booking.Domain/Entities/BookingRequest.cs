using BaseClasses.Entities;
using Booking.Domain.Enums;
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
        public int GuestNo { get; set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public Status Status { get; private set; }
        public ICollection<User> CoTravelers { get; private set; }

        public BookingRequest(long guestId, long accommodationId, int guestNo,
            DateTime startDate, DateTime endDate)
        {
            GuestId = guestId;
            AccommodationId = accommodationId;
            GuestNo = guestNo;
            StartDate = startDate;
            EndDate = endDate;
            Status = Status.Pending;
        }
        public void SetStatus(Status status)
        {
            Status = status;
        }
        public void AddCotraveler(User user)
        {
            CoTravelers.Add(user);
        }
        public void RemoveCotraveler(User user)
        {
            CoTravelers.Remove(user);
        }
        
      
    }
}
