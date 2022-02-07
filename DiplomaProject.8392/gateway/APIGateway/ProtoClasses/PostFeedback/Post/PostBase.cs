using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostFeedback.API
{
    public abstract class PostBase
    {
        public DateTime? MovingInTime { get; set; }
        public DateTime? MovingOutTime { get; set; }
    }
}
