using Grpc.Core;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Post.API
{ 
    public interface IAccommodationItems
    {
        AsyncUnaryCall<ItemsList> GetItemsAsync(Empty request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        AsyncUnaryCall<Response> AddItemsAsync(AddItemsRequest items, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        AsyncUnaryCall<Response> RemoveItemsAsync(RemoveItemsRequest items, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
    }
}
