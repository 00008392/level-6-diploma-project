using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.DTOs
{
    //dto for feedback manipulation 
    public class FeedbackDTO
    {
        public long? CreatorId { get; private set; }
        //accommodation or user on which feedback is left
        public long ItemId { get; private set; }
        public int Rating { get; private set; }
        public string Message { get; private set; }

        public FeedbackDTO(
            long? creatorId,
            long itemId,
            int rating,
            string message)
        {
            CreatorId = creatorId;
            ItemId = itemId;
            Rating = rating;
            Message = message;
        }
    }
}
