
using Domain.Logic.Base.Specifications;
using PostFeedback.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Specifications
{
    public class DatesBookedSpecification : Specification<Post>
    {
        //specification that filters posts by accommdoation availability in specified period of time
        private readonly DateTime _startDate;
        private readonly DateTime _endDate;

        public DatesBookedSpecification(
            DateTime startDate,
            DateTime endDate)
        {
            _startDate = startDate;
            _endDate = endDate;
        }

        public override Expression<Func<Post, bool>> ToExpression()
        {
            //get all posts where booked dates do not overlap with specified range
            //in other words, get all available accommodations for specified date range
            return request => request.Bookings != null ?
                              !request.Bookings.Any(x=>x.StartDate.Date<=_endDate.Date&&_startDate.Date<=x.EndDate.Date)
                              : true;
        }
    }
}
