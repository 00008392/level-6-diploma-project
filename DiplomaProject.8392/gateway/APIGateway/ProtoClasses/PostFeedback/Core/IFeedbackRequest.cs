using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostFeedback.API
{
    //interface to hide creator id property of classes that implement this interface
    //implemented by grpc generated CreateFeedbackRequest class
    public interface IFeedbackRequest
    {
        [JsonIgnore]
        long? CreatorId { get; set; }
    }
}
