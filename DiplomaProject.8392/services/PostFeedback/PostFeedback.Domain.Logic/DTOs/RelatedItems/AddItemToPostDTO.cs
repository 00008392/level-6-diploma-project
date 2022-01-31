using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.DTOs
{
    //DTO for bridge tables between accommodation and rules/facilities/specificities
    public class AddItemToPostDTO
    {
        public long ItemId  { get; private set; }
        public string OtherValue  { get; private set; }

        public AddItemToPostDTO(long itemId, string otherValue)
        {
            ItemId = itemId;
            OtherValue = otherValue;
        }
    }
}
