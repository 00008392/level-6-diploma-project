using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostFeedback.API
{
    //implements interface in order to hide TimeStamp format from user in json format
    public partial class DatesBooked: IDatesBooked
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
