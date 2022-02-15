using APIGateway.Authorization.Helpers;
using APIGateway.Authorization.Requirements.Feedback;
using Microsoft.AspNetCore.Authorization;
using PostFeedback.API;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGateway.Authorization.Handlers.Feedback
{
    //necessary for implementation of resource-based authorization
    //checks if feedback delete requirement is met
    //applies to delete feedback action
    //generic because feedbacks are either for users or for posts
    public class FeedbackDeleteAuthorizationHandler<T> : AuthorizationHandler<FeedbackDeleteRequirement<T>, long>
        where T: IFeedbackService
    {
        //inject grpc service interface to retrieve feedback
        private readonly T _feedbackClient;

        public FeedbackDeleteAuthorizationHandler(T feedbackClient)
        {
            _feedbackClient = feedbackClient;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            FeedbackDeleteRequirement<T> requirement,
            long resource)
        {
            //feedback can be deleted only by its creator
            //if id of logged user and id of feedback creator are the same, then creator can access delete action
            var feedback = await _feedbackClient.GetFeedbackDetailsAsync(new Request { Id = resource });
            if (feedback?.FeedbackCreator?.Id == AuthorizationHelper.GetLoggedUserId(context.User))
            {
                context.Succeed(requirement);
            }
        }
    }
}
