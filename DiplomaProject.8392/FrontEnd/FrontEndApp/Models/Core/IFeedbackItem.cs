using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models.Core
{
    //implemented by user response and post response (feedback items)
    public interface IFeedbackItem
    {
        long Id { get; set; }
    }
}
