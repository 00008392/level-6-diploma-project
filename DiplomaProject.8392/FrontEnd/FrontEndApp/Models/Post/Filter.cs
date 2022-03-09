using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models.Post
{
    //class for holding post filtering criteria
    public class Filter
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
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
