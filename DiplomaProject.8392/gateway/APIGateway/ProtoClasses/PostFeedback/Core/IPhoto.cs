using Google.Protobuf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostFeedback.API
{
    public interface IPhoto
    {
        [JsonIgnore]
        ByteString PhotoByteStr { get; set; }
    }
}
