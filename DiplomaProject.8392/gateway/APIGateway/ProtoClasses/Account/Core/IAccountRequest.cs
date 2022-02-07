using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account.API
{
    //interface implemented by register and update request classes
    //in order to send data in TimeStamp format instead of DateTime 
    public interface IAccountRequest
    {
        DateTime? DateOfBirth { get; set; }
        [JsonIgnore]
        Timestamp DateOfBirthTimeStamp { get; set; }
    }
}
