using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostFeedback.API
{
    //implemented by create and update post requests
    //this interface is needed in order to hide owner id and TimeStamp format from user in json format,
    //since DateTime is more convenient format
    //and in order to convert Datetime format to TimeStamp in controller methods

    public interface IPostRequest
    {
         DateTime? MovingInTime { get; set; }
        [JsonIgnore]
         Timestamp MovingInTimeStamp { get; set; }
         DateTime? MovingOutTime { get; set; }
        [JsonIgnore]
         Timestamp MovingOutTimeStamp { get; set; }
        [JsonIgnore]
         long? OwnerId { get; set; }
    }
}
