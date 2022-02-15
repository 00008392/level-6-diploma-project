using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.DTOs
{
    //dto that holds information on which period of time accommodation is booked
    public class DatesBookedDTO
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public DatesBookedDTO(
            DateTime startDate,
            DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
