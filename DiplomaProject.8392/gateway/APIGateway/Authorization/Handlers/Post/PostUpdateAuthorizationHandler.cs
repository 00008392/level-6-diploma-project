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
    //checks if post update requirement is met
    //applies to update post action
    public class PostUpdateAuthorizationHandler : AuthorizationHandler<PostUpdateRequirement, UpdatePostRequest>
    {
        //inject grpc service to retrieve post
        private readonly PostService.PostServiceClient _postClient;

        public PostUpdateAuthorizationHandler(PostService.PostServiceClient postClient)
        {
            _postClient = postClient;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PostUpdateRequirement requirement,
            UpdatePostRequest resource)
        {
            //post can be modified only by its owner
            //if id of logged user (in request it is set to owner id) and id of post owner are the same, then owner can access update action
            var post = await _postClient.GetPostByIdAsync(new Request { Id = resource.Id } );
            if (post?.Owner?.Id == resource.OwnerId)
            {
                context.Succeed(requirement);
            }
        }
    }
}
