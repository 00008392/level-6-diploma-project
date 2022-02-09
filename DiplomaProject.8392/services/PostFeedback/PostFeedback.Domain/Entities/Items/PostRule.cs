
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Entities
{
    //bridge between post and rule
    //needed because of many to many relationships
    public class PostRule : PostItem
    {

        public PostRule(
            long postId,
            long itemId,
            string otherValue) 
            : base(
                  postId,
                  itemId,
                  otherValue)
        {
        }

    }
}
