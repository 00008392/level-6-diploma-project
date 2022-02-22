using Google.Protobuf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostFeedback.API
{
    //interface to byte string property of classes that implement this interface
    //implemented by grpc generated Photo class
    public interface IPhoto
    {
        [JsonIgnore]
        ByteString PhotoByteStr { get; set; }
    }
}
