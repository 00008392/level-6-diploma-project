using FrontEndApp.Models.Feedback.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models
{
    public class CreateFeedback: FeedbackBase
    {
        public long UserId { get; set; }
        public long ItemId { get; set; }
    }
}
