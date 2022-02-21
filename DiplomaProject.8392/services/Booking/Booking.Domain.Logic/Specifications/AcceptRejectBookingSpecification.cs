using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Logic.Base.Specifications;

namespace Booking.Domain.Logic.Specifications
{
    //this specification checks whether status of booking can be changed to Accepted/Rejected
    //booking can be accepted or rejected minimum 3 days before start date and
    //only if its current status is Pending
    public class AcceptRejectBookingSpecification : Specification<Entities.Booking>
    {
        //check whether current status is Pending
        public override Expression<Func<Entities.Booking, bool>> ToExpression()
        {
            return request => request.Status == Enums.Status.Pending && 
                (request.StartDate-DateTime.Now).Days>=3;
        }
    }
}
