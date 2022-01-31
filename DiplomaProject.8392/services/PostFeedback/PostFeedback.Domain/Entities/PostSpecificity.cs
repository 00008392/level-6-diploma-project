
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Entities
{
    public class PostSpecificity : PostItem
    {

        public PostSpecificity(long postId, long itemId, string otherValue) 
            : base(postId, itemId, otherValue)
        {
        }

    }
}
