using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Filter
{
    //criteria by which posts can be queried
    public class FilterParameters
    {
        //search by text in title or description
        public string SearchText { get; set; }
        public long? Owner { get; set; }
        public long? Category { get; set; }
        public long? City { get; set; }
        //filter by number of rooms range
        public int? MinRooms { get; set; }
        public int? MaxRooms { get; set; }
        //filter by number of beds range
        public int? MinBeds { get; set; }
        public int? MaxBeds { get; set; }
        //get all posts where maximum number of guests is greater than specified guest number
        public int? Guests { get; set; }
        //filter by price range
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public bool? EntireApartment { get; set; }
    }
}
