using BaseClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Entities
{
    //represents accepted booking on accommodation
    public class Booking: BaseEntity
    {
        //id of post in which information about booked accommodation is stored
        public long PostId { get;private set; }
        public Post Post { get;private set; }
        //user who booked accommodation
        public long GuestId { get; private set; }
        public User Guest { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public Booking(
            long id,
            long postId,
            long guestId,
            DateTime startDate,
            DateTime endDate)
            :base(id)
        {
            PostId = postId;
            GuestId = guestId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
