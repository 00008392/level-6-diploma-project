using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.API
{
    //interface for hiding property Guest id from user 
    //value to this property will be set in controller method
    public interface ICreateRequest
    {
        [JsonIgnore]
        long? GuestId { get; set; }
    }
}
