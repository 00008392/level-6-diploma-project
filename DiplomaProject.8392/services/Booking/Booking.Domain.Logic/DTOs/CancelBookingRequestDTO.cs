using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.DTOs
{
   public class CancelBookingRequestDTO
    {
        public long RequestId { get;private set; }
        public long UserId { get;private set; }

        public CancelBookingRequestDTO(long requestId, long userId)
        {
            RequestId = requestId;
            UserId = userId;
        }
    }
}
