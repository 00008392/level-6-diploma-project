
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostFeedback.API
{
    //IPostRequest - in order to hide properties from user indicated in the interface
    public partial class PostResponse: IPostResponse
    {
        public DateTime DatePublished { get; set; }
    }
}
