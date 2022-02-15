using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGateway.Authorization.Requirements.Post
{
    //necessary for implementation of resource-based authorization
    //applies to post modification
    public class PostUpdateRequirement: IAuthorizationRequirement
    {
    }
}
