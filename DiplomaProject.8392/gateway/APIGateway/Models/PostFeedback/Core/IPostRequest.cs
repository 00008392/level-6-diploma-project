using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostFeedback.API
{
    public interface IPostRequest
    {
        public DateTime? MovingInTime { get; set; }
        public Timestamp MovingInTimeStamp { get; set; }
        public DateTime? MovingOutTime { get; set; }
        public Timestamp MovingOutTimeStamp { get; set; }
    }
}
