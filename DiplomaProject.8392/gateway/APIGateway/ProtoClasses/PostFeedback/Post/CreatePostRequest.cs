using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostFeedback.API
{
    //IPostRequest - in order to send data in TimeStamp format to the service instead of DateTime 
    //and in order to hide properties from user indicated in the interface
    public partial class CreatePostRequest: PostRequestBase, IPostRequest
    {
    }
}
