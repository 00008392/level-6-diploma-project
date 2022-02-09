using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Entities
{
    //bridge between post and facility
    //needed because of many to many relationships
    public class PostFacility : PostItem
    {
        public PostFacility(
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
