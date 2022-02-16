using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostFeedback.API
{
    //this interface is needed in order to hide TimeStamp format from user in json format
    public interface IDatesBooked
    {
        [JsonIgnore]
        Timestamp StartDateTimeStamp { get; set; }
        [JsonIgnore]
        Timestamp EndDateTimeStamp { get; set; }
    }
}
