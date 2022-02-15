using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostFeedback.API
{
    //implements IFeedbackRequest interface to hide creator id property
    public partial class CreateFeedbackRequest: IFeedbackRequest
    {
    }
}
