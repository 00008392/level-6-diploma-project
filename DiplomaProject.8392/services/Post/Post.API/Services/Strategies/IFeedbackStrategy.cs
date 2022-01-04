using Post.Domain.Core;
using Post.Domain.Logic.DTOs.Core;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post.API.Services.Strategies
{
    public interface IFeedbackStrategy<T, E> where T: FeedbackEntity
                                             where E: IFeedbackEntityDTO
    {
        Task<Response> LeaveFeedbackAsync(CreateFeedbackRequest request);
        Task<Response> DeleteFeedbackAsync(Request request);
        Task<FeedbackInfoResponse> GetFeedbackDetailsAsync(Request request);
        Task<FeedbacksListResponse> GetFeedbacksAsync(Request request);
    }
}
