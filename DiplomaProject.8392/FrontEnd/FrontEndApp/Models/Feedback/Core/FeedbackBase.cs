using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models.Feedback.Core
{
    public class FeedbackBase: ErrorViewModel
    {
        [Required]
        public int Rating { get; set; }
        public string Message { get; set; }
    }
}
