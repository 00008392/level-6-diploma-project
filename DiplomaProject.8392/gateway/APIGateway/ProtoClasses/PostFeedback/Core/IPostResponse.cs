using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostFeedback.API
{
    //implemented by post response
    //this interface is needed in order to hide TimeStamp format from user in json format,
    //since DateTime is more convenient format
    //and in order to convert TimeStamp format to DateTime in controller methods
    public interface IPostResponse
    {
        DateTime DatePublished { get; set; }
        [JsonIgnore]
        Timestamp DatePublishedTimeStamp { get; set; }
    }
}
