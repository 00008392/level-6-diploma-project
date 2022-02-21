using APIGateway.Authorization.Requirements.Booking;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostFeedback.API;
using APIGateway.Authorization.Helpers;
using Protos.Common;

namespace APIGateway.Authorization.Handlers.Booking
{
    //necessary for implementation of resource-based authorization
    //checks if get bookings by post requirement is met
    //applies to retrieval of bookings by post id
    public class GetBookingsByPostAuthorizationHandler :
         AuthorizationHandler<GetBookingsByPostRequirement, long>
    {
        //inject post grpc service to retrieve post info
        private readonly PostService.PostServiceClient _postClient;
        public GetBookingsByPostAuthorizationHandler(PostService.PostServiceClient postClient)
        {
            _postClient = postClient;
        }
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            GetBookingsByPostRequirement requirement,
            long resource)
        {
            //only owner of post can view bookings made on accommodation mentioned in post
            //if id of logged user and id of post owner are the same
            //then user can view bookings
            var post = await _postClient.GetPostByIdAsync(new Request { Id = resource });
            if (AuthorizationHelper.GetLoggedUserId(context.User) == post.Owner?.Id)
            {
                context.Succeed(requirement);
            }
        }
    }
}
