using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostFeedback.API
{
    //implements interface in order to be used in controller method 
    public partial class CreatePostRequest :BasePostRequest, IPostRequest
    {
    }
}
