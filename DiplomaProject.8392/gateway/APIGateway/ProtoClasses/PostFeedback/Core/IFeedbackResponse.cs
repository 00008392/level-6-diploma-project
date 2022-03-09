using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostFeedback.API
{
    //implemented by feedback response
    //needed to hide certain properties of response 
    public interface IFeedbackResponse
    {
        DateTime DatePublished { get; set; }
        [JsonIgnore]
        Timestamp DatePublishedTimeStamp { get; set; }
    }
}
