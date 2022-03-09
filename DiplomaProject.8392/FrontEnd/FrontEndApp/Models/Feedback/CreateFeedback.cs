using FrontEndApp.Models.Feedback.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models.Feedback
{
    public class CreateFeedback: FeedbackBase
    {
        public long ItemId { get; set; }
    }
}
