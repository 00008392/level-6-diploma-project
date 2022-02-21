using Booking.Domain.Enums;
using Booking.Domain.Logic.Specifications;
using Domain.Logic.Base.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.DTOs
{
    //dto for booking status update
    public class UpdateBookingStatusDTO
    {
        public long BookingId { get;private set; }
        //depending on status that should be set to booking, there can be different constraints
        //for example, Accepted status can be set only if its status was Pending, etc.
        public Specification<Entities.Booking> BookingSpecification { get;private set; }
        //new status
        public Status Status { get;private set; }

        public UpdateBookingStatusDTO(
            long bookingId,
            Specification<Entities.Booking> bookingSpecification,
            Status bookingStatus)
        {
            BookingId = bookingId;
            BookingSpecification = bookingSpecification;
            Status = bookingStatus;
        }
    }

}
