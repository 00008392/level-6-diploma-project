using PostFeedback.Domain.Entities;
using PostFeedback.Domain.Logic.DTOs;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostFeedback.API.Services.Strategies
{
    //generic strategy for feedback handling
    //needed for user feedback and post feedback handling to reduce code duplication
    public interface IFeedbackStrategy<TEntity, TDTO> where TEntity : FeedbackEntity
                                             where TDTO : IFeedbackEntityDTO
    {
        Task<Response> LeaveFeedbackAsync(CreateFeedbackRequest request);
        Task<Response> DeleteFeedbackAsync(Request request);
        Task<FeedbackResponse> GetFeedbackDetailsAsync(Request request);
        Task<FeedbackListResponse> GetFeedbacksForItemAsync(Request request);
    }
}
