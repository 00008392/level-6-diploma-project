using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.DTOs
{
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
