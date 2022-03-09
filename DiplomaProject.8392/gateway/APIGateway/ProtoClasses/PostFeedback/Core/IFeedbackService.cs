using Grpc.Core;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PostFeedback.API
{
    //interface needed for generic feedback controller and generic authorization handlers
    //implemented by grpc feedback services (for user and for post)
    public interface IFeedbackService
    {
        AsyncUnaryCall<Response> LeaveFeedbackAsync(CreateFeedbackRequest request, Metadata headers = null, 
            DateTime? deadline = null, CancellationToken cancellationToken = default);
        AsyncUnaryCall<Response> DeleteFeedbackAsync(Request request, Metadata headers = null,
            DateTime? deadline = null, CancellationToken cancellationToken = default);
        AsyncUnaryCall<FeedbackResponse> GetFeedbackDetailsAsync(Request request, Metadata headers = null,
            DateTime? deadline = null, CancellationToken cancellationToken = default);
        AsyncUnaryCall<FeedbackListResponse> GetFeedbacksForItemAsync(Request request, Metadata headers = null, 
            DateTime? deadline = null, CancellationToken cancellationToken = default);
        AsyncUnaryCall<AverageRatingResponse> GetAverageRatingAsync(Request request, Metadata headers = null,
           DateTime? deadline = null, CancellationToken cancellationToken = default);
        AsyncUnaryCall<CanLeaveFeedbackResponse> CanLeaveFeedbackAsync(CanLeaveFeedbackRequest request,
            Metadata headers = null,
            DateTime? deadline = null, CancellationToken cancellationToken = default);
    }
}
