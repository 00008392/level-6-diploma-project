using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.Specification
{
    public class BookingsByUserSpecification : BookingRequestSpecification
    {
        private readonly long _userId;
        public BookingsByUserSpecification(long id)
        {
            _userId = id;
        }
        public override Expression<Func<BookingRequest, bool>> ToExpression()
        {
            return request => request.GuestId == _userId || request.CoTravelers.Any(x => x.Id == _userId);
        }
    }
}
