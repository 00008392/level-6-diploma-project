using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGateway.Helpers
{
    //helper class to convert DateTime to TimeStamp and vice versa
    public static class DateTimeConversion
    {
        public static Timestamp FromDateTimeToTimeStamp(DateTime? time)
        {
            return time == null ? null : Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)time, DateTimeKind.Utc));
        }
        public static DateTime? FromTimeStampToDateTime(Timestamp time)
        {
            return time?.ToDateTime();
        }
    }
}
