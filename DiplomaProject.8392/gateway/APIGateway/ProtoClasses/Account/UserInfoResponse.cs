
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Account.API
{
    //display dates in DateTime format instead of TimeStamp
    //implement interface in order to enable JsonIgnore attributes 
    public partial class UserInfoResponse: IUserInfoResponse
    {
        public DateTime RegistrationDate { get; set; }
        public DateTime DateOfBirth { get; set; }

    }
}
