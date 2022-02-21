using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Enums
{
    //status that booking can have
    public enum Status
    {
        //default, when user just created booking and it is not accepted or rejected yet
        Pending,
        Accepted,
        Rejected,
        //can be cancelled if its status is accepted
        Cancelled
    }
}
