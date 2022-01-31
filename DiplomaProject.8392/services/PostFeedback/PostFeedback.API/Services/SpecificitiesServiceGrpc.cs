using Grpc.Core;
using PostFeedback.API.Services.Strategies;
using PostFeedback.Domain.Entities;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostFeedback.API.Services
{
    public class SpecificitiesServiceGrpc : PostSpecificities.PostSpecificitiesBase
    {
        public SpecificitiesServiceGrpc(IPostItemsStrategy<PostSpecificity, Specificity> strategy)
            : base(strategy)
        {

        }
        public override async Task<ItemsList> GetItems(Empty request, ServerCallContext context)
        {
            return await GetItems();
        }
        public override async Task<Response> AddItems(AddItemsRequest request, ServerCallContext context)
        {

            return await _strategy.AddItemsAsync(request);
        }
        public override async Task<Response> RemoveItems(RemoveItemsRequest request, ServerCallContext context)
        {

            return await _strategy.RemoveItemsAsync(request);
        }
    }
}
