using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostFeedback.API
{
    //interface to hide time stamp properties in post requests
    public interface IPostRequest
    {
        [JsonIgnore]
        Timestamp MovingInTimeStamp { get; set; }
        [JsonIgnore]
        Timestamp MovingOutTimeStamp { get; set; }
        DateTime? MovingInTime { get; set; }
        DateTime? MovingOutTime { get; set; }
    }
}
