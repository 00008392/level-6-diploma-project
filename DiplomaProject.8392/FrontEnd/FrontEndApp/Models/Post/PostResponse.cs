using FrontEndApp.Models.Post.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models.Post
{
    public class PostResponse: PostBase
    {
        public Owner Owner { get; set; }
        public string Category { get; set; }
        public string City { get; set; }
        public string MovingInTime { get; set; }
        public string MovingOutTime { get; set; }
        public string CoverPhoto { get; set; }
        public DateTime DatePublished { get; set; }
        public Item[] Rules { get; set; }
        public Item[] Facilities { get; set; }
        public Item[] DatesBooked { get; set; }
        public bool NoItem { get; set; } = false;
    }
}
