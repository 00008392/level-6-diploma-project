using PostFeedback.Domain.Entities;
using PostFeedback.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Contracts
{
    public interface IFeedbackValidationService<T> where T: FeedbackEntity
    {
        Task<bool> CanLeaveFeedback(FeedbackDTO feedback);
    }
}
