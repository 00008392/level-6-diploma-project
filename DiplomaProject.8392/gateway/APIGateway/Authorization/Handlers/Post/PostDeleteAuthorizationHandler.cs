using APIGateway.Authorization.Helpers;
using APIGateway.Authorization.Requirements.Post;
using Microsoft.AspNetCore.Authorization;
using PostFeedback.API;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGateway.Authorization.Handlers.Post
{
    //necessary for implementation of resource-based authorization
    //checks if post delete requirement is met
    //applies to delete post action
    public class PostDeleteAuthorizationHandler : AuthorizationHandler<PostDeleteRequirement, long>
    {
        //inject grpc service to retrieve post
        private readonly PostService.PostServiceClient _postClient;

        public PostDeleteAuthorizationHandler(PostService.PostServiceClient postClient)
        {
            _postClient = postClient;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PostDeleteRequirement requirement,
            long resource)
        {
            //post can be deleted only by its owner
            //if id of logged user and id of post owner are the same, then owner can access delete action
            var post = await _postClient.GetPostByIdAsync(new Request { Id = resource });
            if (post?.Owner?.Id == AuthorizationHelper.GetLoggedUserId(context.User))
            {
                context.Succeed(requirement);
            }
        }
    }
}
