using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostFeedback.API.Mappings.Helpers
{
    public static class ConversionHelper
    {
        //method that converts datetime format to string format that displays hours and minutes
        public static string DateTimeToString(DateTime? time)
        {
            return time?.ToString("HH:mm");
        }
    }
}
