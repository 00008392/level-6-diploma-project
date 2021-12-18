using BaseClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Entities
{
    public class DatesBooked: BaseEntity
    {
        public long AccommodationId { get;private set; }
        public Accommodation Accommodation { get;private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public DatesBooked(
            long id,
            long accommodationId,
            DateTime startDate,
            DateTime endDate): base(id)
        {
            AccommodationId = accommodationId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
