using Grpc.Core;
using Post.API;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Post.API
{
    public interface IFeedbackService
    {
        AsyncUnaryCall<Response> LeaveFeedbackAsync(CreateFeedbackRequest request, Metadata headers = null, 
            DateTime? deadline = null, CancellationToken cancellationToken = default);
        AsyncUnaryCall<Response> DeleteFeedbackAsync(Request request, Metadata headers = null,
            DateTime? deadline = null, CancellationToken cancellationToken = default);
        AsyncUnaryCall<FeedbackInfoResponse> GetFeedbackDetailsAsync(Request request, Metadata headers = null,
            DateTime? deadline = null, CancellationToken cancellationToken = default);
        AsyncUnaryCall<FeedbacksListResponse> GetFeedbacksAsync(Request request, Metadata headers = null, 
            DateTime? deadline = null, CancellationToken cancellationToken = default);
    }
}
