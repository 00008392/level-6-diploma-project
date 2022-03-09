using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models.Feedback
{
    public class AverageRatingResponse
    {
        public double? Rating { get; set; }
        public bool? NoRating { get; set; }
    }
}
