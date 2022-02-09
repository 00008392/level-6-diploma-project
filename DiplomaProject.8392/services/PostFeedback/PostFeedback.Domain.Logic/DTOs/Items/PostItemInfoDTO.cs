using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.DTOs
{
    //dto for holding information about item specific to post
    //bridge between post and item
    public class PostItemInfoDTO: AddItemToPostDTO
    {
        public string Item { get; private set; }

        public PostItemInfoDTO(long itemId, string otherValue, string item)
            :base(itemId, otherValue)
        {
            Item = item;
        }
    }
}
