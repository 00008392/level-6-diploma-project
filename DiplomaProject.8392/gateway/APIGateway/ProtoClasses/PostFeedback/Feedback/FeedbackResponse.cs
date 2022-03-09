using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostFeedback.API
{
    //implements IFeedbackResponse interface to hide time stamp property and display date in 
    //date time format
    public partial class FeedbackResponse : IFeedbackResponse
    {
        public DateTime DatePublished { get; set; }
    }
}
