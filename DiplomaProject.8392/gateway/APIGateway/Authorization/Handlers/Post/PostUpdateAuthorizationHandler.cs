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
    //applies to update post, delete post, add photos to post and remove photos from post actions
    public class PostUpdateAuthorizationHandler : AuthorizationHandler<PostUpdateRequirement, long>
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
            long resource)
        {
            //post can be manipulated only by its owner
            //if id of logged user (in request it is set to owner id) and id of post owner are the same, then owner can access actions
            var post = await _postClient.GetPostByIdAsync(new Request { Id = resource } );
            if (post?.Owner?.Id == AuthorizationHelper.GetLoggedUserId(context.User))
            {
                context.Succeed(requirement);
            }
        }
    }
}
