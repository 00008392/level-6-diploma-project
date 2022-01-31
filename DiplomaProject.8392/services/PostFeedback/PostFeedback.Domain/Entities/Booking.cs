using BaseClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Entities
{
    public class Booking: BaseEntity
    {
        public long PostId { get;private set; }
        public Post Post { get;private set; }
        public long UserId { get; private set; }
        public User User { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public Booking(
            long id,
            long postId,
            long userId,
            DateTime startDate,
            DateTime endDate):base(id)
        {
            PostId = postId;
            UserId = userId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
