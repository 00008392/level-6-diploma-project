using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post.API
{
    public abstract class PostBase
    {
        public DateTime? MovingInTime { get; set; }
        public DateTime? MovingOutTime { get; set; }
    }
}
