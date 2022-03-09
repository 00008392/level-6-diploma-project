using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models.Feedback.Core
{
    //base feedback class
    public class FeedbackBase
    {
        [Required(ErrorMessage = "Rating is required")]
        [Range(1, 5, ErrorMessage = "Rating value should be between 1 and 5")]
        public int Rating { get; set; }
        [MaxLength(600, ErrorMessage = "Maximum length of feedback message is 600 symbols")]
        public string Message { get; set; }
    }
}
