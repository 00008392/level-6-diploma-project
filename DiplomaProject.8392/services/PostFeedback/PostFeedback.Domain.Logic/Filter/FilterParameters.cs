using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Filter
{
    public class FilterParameters
    {
        public string SearchText { get; set; }
        public long? Owner { get; set; }
        public long? Category { get; set; }
        public long? City { get; set; }
        public int? MinRooms { get; set; }
        public int? MaxRooms { get; set; }
        public int? MinBeds { get; set; }
        public int? MaxBeds { get; set; }
        public int? Guests { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public bool? EntireApartment { get; set; }
    }
}
