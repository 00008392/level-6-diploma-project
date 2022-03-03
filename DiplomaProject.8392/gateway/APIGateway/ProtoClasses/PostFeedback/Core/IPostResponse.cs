using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostFeedback.API
{
    //implemented by post response
    //needed to hide certain properties of response and for controller method
    public interface IPostResponse
    {
        DateTime DatePublished { get; set; }
        [JsonIgnore]
        Timestamp DatePublishedTimeStamp { get; set; }
    }
}
