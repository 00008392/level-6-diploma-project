using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostFeedback.API
{
    //base class for create and update requests
    public abstract class PostRequestBase
    {
        public DateTime? MovingInTime { get; set; }
        public DateTime? MovingOutTime { get; set; }
    }
}
