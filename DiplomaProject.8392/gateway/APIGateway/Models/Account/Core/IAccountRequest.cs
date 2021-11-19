using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account.API
{
    public interface IAccountRequest
    {
        public DateTime? DateOfBirth { get; set; }
        public Timestamp DateOfBirthTimeStamp { get; set; }
    }
}
