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
    //this specification allows to filter bookings by post which has information about accommodation
    //that is booked
    public class BookingsByPostSpecification : Specification<Entities.Booking>
    {
        private readonly long _postId;
        public BookingsByPostSpecification(long id)
        {
            _postId = id;
        }
        public override Expression<Func<Entities.Booking, bool>> ToExpression()
        {
            //get bookings by post id
            return request => request.PostId == _postId;
        }
    }
}
