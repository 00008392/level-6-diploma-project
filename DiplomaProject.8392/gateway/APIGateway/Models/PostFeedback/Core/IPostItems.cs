using Grpc.Core;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PostFeedback.API
{ 
    public interface IPostItems
    {
        AsyncUnaryCall<ItemsList> GetItemsAsync(Empty request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        AsyncUnaryCall<Response> AddItemsAsync(AddItemsRequest items, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        AsyncUnaryCall<Response> RemoveItemsAsync(RemoveItemsRequest items, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
    }
}
