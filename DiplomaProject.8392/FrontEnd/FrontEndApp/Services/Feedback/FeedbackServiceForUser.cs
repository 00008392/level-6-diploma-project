using FrontEndApp.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontEndApp.Services.Feedback
{
    //service that consumes user feedback api
    public class FeedbackServiceForUser: FeedbackServiceBase<UserResponse>
    {
        public FeedbackServiceForUser(HttpClient client)
            :base(client, "api/user-feedbacks")
        {

        }
    }
}
