using FrontEndApp.Models.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontEndApp.Services.Feedback
{
    //service that consumes post feedback api
    public class FeedbackServiceForPost: FeedbackServiceBase<PostResponse>
    {
        public FeedbackServiceForPost(HttpClient client)
            :base(client, "api/post-feedbacks")
        {

        }
    }
}
