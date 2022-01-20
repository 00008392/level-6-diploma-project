using BaseClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Entities
{
    public class Booking: BaseEntity
    {
        public long AccommodationId { get;private set; }
        public Accommodation Accommodation { get;private set; }
        public long UserId { get; private set; }
        public User User { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public Booking(
            long id,
            long accommodationId,
            long userId,
            DateTime startDate,
            DateTime endDate):base(id)
        {
            AccommodationId = accommodationId;
            UserId = userId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
