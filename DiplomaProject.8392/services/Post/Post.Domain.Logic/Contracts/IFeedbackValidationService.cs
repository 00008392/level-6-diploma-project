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
    public interface IFeedbackValidationService<T> where T: FeedbackEntity
    {
        Task<bool> CanLeaveFeedback(FeedbackDTO feedback);
    }
}
