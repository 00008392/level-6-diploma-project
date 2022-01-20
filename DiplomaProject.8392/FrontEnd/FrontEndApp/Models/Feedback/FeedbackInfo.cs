using FrontEndApp.Models.Feedback.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models
{
    public class FeedbackInfo: FeedbackBase
    {
        public long Id { get; set; }
        public UserInfo FeedbackOwner { get; set; }
    }
}
