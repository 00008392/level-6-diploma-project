using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.DTOs
{
    //DTO for adding item (rule/facility) to accommodation specified in post
    public class AddItemToPostDTO
    {
        public long ItemId  { get; private set; }
        //if custom item is defined by user, value is stored in OtherValue
        public string OtherValue  { get; private set; }

        public AddItemToPostDTO(
            long itemId,
            string otherValue)
        {
            ItemId = itemId;
            OtherValue = otherValue;
        }
    }
}
