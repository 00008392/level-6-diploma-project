using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.DTOs
{
    public class UpdateRequestDTO
    {
        public long Id { get;private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public UpdateRequestDTO(long id, DateTime startDate, DateTime endDate)
        {
            Id = id;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
