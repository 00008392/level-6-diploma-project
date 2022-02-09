using BaseClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Entities
{
    //booking entity is needed in the account microservice because user
    //(either guest or owner) cannot delete account if he/she has active bookings 
    public class Booking: BaseEntity
    {
        public long GuestId { get;private set; }
        public User Guest { get; private set; }
        public long OwnerId { get; private set; }
        public User Owner { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public Booking(
            long id,
            long guestId,
            long ownerId,
            DateTime startDate,
            DateTime endDate)
            :base(id)
        {
            GuestId = guestId;
            OwnerId = ownerId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
