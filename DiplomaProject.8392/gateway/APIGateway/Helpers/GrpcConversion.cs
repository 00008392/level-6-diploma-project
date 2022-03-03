using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGateway.Helpers
{
    //helper class to convert data types to types accepted by grpc
    public static class GrpcConversion
    {
        //convert date time to type stamp
        public static Timestamp FromDateTimeToTimeStamp(DateTime? time)
        {
            return time == null ? null : Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)time, DateTimeKind.Utc));
        }
        //convert time stamp to date time
        public static DateTime? FromTimeStampToDateTime(Timestamp time)
        {
            return time?.ToDateTime();
        }
        //convert byte array to byte string
        public static ByteString FromByteArrayToByteString(byte[] bytes)
        {
            return bytes == null ? null : ByteString.CopyFrom(bytes);
        }
    }
}
