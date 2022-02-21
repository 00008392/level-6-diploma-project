using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.API
{
    //interface for hiding timestamp properties in booking related grpc classes
    //and displaying them as date time in json format
    public interface IBooking
    {
        [JsonIgnore]
        Timestamp StartDateTimeStamp { get; set; }
        [JsonIgnore]
        Timestamp EndDateTimeStamp { get; set; }
        DateTime? StartDate { get; set; }
        DateTime? EndDate { get; set; }
    }
}
