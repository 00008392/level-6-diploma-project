using FrontEndApp.Models.Core;
using FrontEndApp.Services.Feedback.Contracts;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Components
{
    //component that all feedback components inherit from
    public class FeedbackBaseComponent<T> : CustomBaseComponent where T: IFeedbackItem
    {
        [Inject]
        protected IFeedbackService<T> _service { get; set; }
    }
}
