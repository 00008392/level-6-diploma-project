using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.DTOs
{
    //dto for holding information about feedback
    //T - either user or accommodation
   public class FeedbackInfoDTO<T>: FeedbackDTO where T:IFeedbackEntityDTO
    {
        public long Id { get; private set; }
        //accommodation or user on which feedback is left
        public T Item { get; private set; }
        public UserDTO FeedbackCreator { get; private set; }
        public DateTime DatePublished { get; private set; }
        public FeedbackInfoDTO(
             long? userId,
             long itemId,
             int rating,
             string message,
             long id,
             T item,
             DateTime datePublished,
             UserDTO feedbackCreator) :
             base(
                 userId,
                 itemId,
                 rating,
                 message)
        {
            Id = id;
            Item = item;
            FeedbackCreator = feedbackCreator;
            DatePublished = datePublished;
        }
    }
}
