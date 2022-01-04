using Post.Domain.Core;
using Post.Domain.Logic.DTOs;
using Post.Domain.Logic.DTOs.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.Contracts
{
    public interface IFeedbackService<T, E> where T: FeedbackEntity
                                            where E: IFeedbackEntityDTO
    {
        Task LeaveFeedbackAsync(FeedbackDTO feedback);
        Task DeleteFeedbackAsync(long id);
        Task<ICollection<FeedbackInfoDTO<E>>> GetFeedbacksAsync(long itemId);
        Task<FeedbackInfoDTO<E>> GetFeedbackDetailsAsync(long id);
    }
}
