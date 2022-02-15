using Microsoft.AspNetCore.Authorization;
using PostFeedback.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGateway.Authorization.Requirements.Feedback
{
    //necessary for implementation of resource-based authorization
    //applies to post and user feedback deletion
    public class FeedbackDeleteRequirement<T>: IAuthorizationRequirement
        where T: IFeedbackService
    {
    }
}
