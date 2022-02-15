using PostFeedback.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostFeedback.Domain.Entities;

namespace PostFeedback.Domain.Logic.Contracts
{
    //service for feedback manipulations and retrieval
    //TEntity - user/post
    //TDTO - userDTO, postDetailsDTO
    public interface IFeedbackService<TEntity, TDTO> where TEntity : FeedbackEntity
                                            where TDTO : IFeedbackEntityDTO
    {
        Task LeaveFeedbackAsync(FeedbackDTO feedback);
        Task DeleteFeedbackAsync(long id);
        //retrieve feedbacks for specific user/accommodation
        //(itemId - id of user/accommodation)
        Task<ICollection<FeedbackInfoDTO<TDTO>>> GetFeedbacksForItemAsync(long itemId);
        //retrieve specific feedback by id
        Task<FeedbackInfoDTO<TDTO>> GetFeedbackDetailsAsync(long id);
    }
}
