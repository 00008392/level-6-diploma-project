using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.API
{
    //implements IBooking for proper display of dates
    //and ICreateRequest to hide guest id property
    public partial class CreateRequest: IBooking, ICreateRequest
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        
    }
}
