using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Account.API
{
    //this interface is needed in order to hide TimeStamp format from user in json format
    //since DateTime is more convenient format
    public interface IUserInfoResponse
    {
        [JsonIgnore]
        Timestamp RegistrationDateTimeStamp { get; set; }
        [JsonIgnore]
        Timestamp DateOfBirthTimeStamp { get; set; }
    }
}
