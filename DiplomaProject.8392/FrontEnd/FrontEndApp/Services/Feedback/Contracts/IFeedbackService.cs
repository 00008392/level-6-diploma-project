using FrontEndApp.Models;
using FrontEndApp.Models.Core;
using FrontEndApp.Models.Feedback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Services.Feedback.Contracts
{
    //service that consumes feedback api
    public interface IFeedbackService<T> where T: IFeedbackItem
    {
        Task<ICollection<FeedbackResponse>> GetFeedbacksForItemAsync(long id);
        Task<double?> GetAverageRatingAsync(long itemId);
        Task<Response> LeaveFeedbackAsync(CreateFeedback feedback, Action onSuccessAction = null,
            Action onErrorAction = null);
        Task<Response> DeleteFeedbackAsync(long id, Action onSuccessAction = null,
            Action onErrorAction = null);
        Task<bool> CanLeaveFeedbackAsync(long itemId);
    }
}
