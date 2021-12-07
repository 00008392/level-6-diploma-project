using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.Specification
{
    public class CancelBookingRequestSpecification : BookingRequestSpecification
    {
        public override Expression<Func<BookingRequest, bool>> ToExpression()
        {
            return request => request.Status == Domain.Enums.Status.Accepted;
        }
    }
}
