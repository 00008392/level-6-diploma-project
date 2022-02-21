using Booking.Domain.Enums;
using Domain.Logic.Base.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.Specifications
{
    //specification that checks whether accommodation indicated in post 
    //is available for dates indicated in booking
    public class BookingsByPostAndDatesSpecification : Specification<Entities.Booking>
    {
        private readonly DateTime _startDate;
        private readonly DateTime _endDate;
        private readonly long _postId;

        public BookingsByPostAndDatesSpecification(
            DateTime startDate,
            DateTime endDate,
            long postId)
        {
            _startDate = startDate;
            _endDate = endDate;
            _postId = postId;
        }

        public override Expression<Func<Entities.Booking, bool>> ToExpression()
        {
            //get bookings with given post id and date range that overlaps with specified range
            return request => request.PostId == _postId &&
              (request.StartDate.Date <= _endDate.Date && _startDate.Date <= request.EndDate.Date)
              && request.Status == Status.Accepted;
        }
    }
}
