using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Account.API
{
    //need this iterface to set properties that should be ignored by json
    public interface IUserInfoResponse
    {
        [JsonIgnore]
        Timestamp RegistrationDateTimeStamp { get; set; }
        [JsonIgnore]
        Timestamp DateOfBirthTimeStamp { get; set; }
    }
}
