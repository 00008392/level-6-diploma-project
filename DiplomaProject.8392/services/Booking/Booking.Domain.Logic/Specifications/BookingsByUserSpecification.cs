using Booking.Domain.Entities;
using Domain.Logic.Base.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.Specifications
{
    //this specification allows to filter bookings by user who created these bookings
    public class BookingsByUserSpecification : Specification<Entities.Booking>
    {
        private readonly long _userId;
        public BookingsByUserSpecification(long id)
        {
            _userId = id;
        }
        public override Expression<Func<Entities.Booking, bool>> ToExpression()
        {
            //get bookings by guest id
            return request => request.GuestId == _userId;
        }
    }
}
