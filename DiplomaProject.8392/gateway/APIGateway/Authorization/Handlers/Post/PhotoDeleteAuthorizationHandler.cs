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
    //checks if photo delete requirement is met
    public class PhotoDeleteAuthorizationHandler: AuthorizationHandler<PhotoDeleteRequirement, long>
    {
        //inject grpc service to retrieve post
        private readonly PostService.PostServiceClient _postClient;
        //inject grpc service to retrieve photo
        private readonly PostPhotoService.PostPhotoServiceClient _photoClient;

        public PhotoDeleteAuthorizationHandler(
            PostService.PostServiceClient postClient,
            PostPhotoService.PostPhotoServiceClient photoClient)
        {
            _postClient = postClient;
            _photoClient = photoClient;
        }
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PhotoDeleteRequirement requirement,
            long resource)
        {
            //photo of post can be deleted by post owner
            //retrieve photo by id to get id of post
            var photo = await _photoClient.GetPhotoAsync(new Request { Id = resource });
            //get post by id retireved from photo
            var post = await _postClient.GetPostByIdAsync(new Request { Id = photo.PostId??0 });
            //check if logged user is owner of post
            if (post?.Owner?.Id == AuthorizationHelper.GetLoggedUserId(context.User))
            {
                context.Succeed(requirement);
            }
        }
    }
}
