using PostFeedback.Domain.Entities;
using PostFeedback.Domain.Logic.DTOs;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostFeedback.API.Services.Strategies
{
    public interface IFeedbackStrategy<T, E> where T: FeedbackEntity
                                             where E: IFeedbackEntityDTO
    {
        Task<Response> LeaveFeedbackAsync(CreateFeedbackRequest request);
        Task<Response> DeleteFeedbackAsync(Request request);
        Task<FeedbackResponse> GetFeedbackDetailsAsync(Request request);
        Task<FeedbacksListResponse> GetFeedbacksAsync(Request request);
    }
}
