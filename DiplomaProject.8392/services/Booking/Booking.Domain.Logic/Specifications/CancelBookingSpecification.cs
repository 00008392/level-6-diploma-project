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
    //this specification checks whether status of booking can be changed to Cancelled
    //booking can be cancelled minimum 3 days before start date
    //and only if its current status is Accepted
    public class CancelBookingSpecification : Specification<Entities.Booking>
    {
        //check that current booking status is accepted
        public override Expression<Func<Entities.Booking, bool>> ToExpression()
        {
            return request => request.Status == Enums.Status.Accepted&&
            (request.StartDate - DateTime.Now).Days >= 3;
        }
    }
}
