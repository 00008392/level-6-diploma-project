using Booking.Domain.Enums;
using Booking.Domain.Logic.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.DTOs
{
    public class BookingStatusDTO
    {
        public long BookingId { get;private set; }
        public BookingRequestSpecification BookingSpecification { get;private set; }
        public Status BookingStatus { get;private set; }

        public BookingStatusDTO(
            long bookingId,
            BookingRequestSpecification bookingSpecification,
            Status bookingStatus)
        {
            BookingId = bookingId;
            BookingSpecification = bookingSpecification;
            BookingStatus = bookingStatus;
        }
    }

}
