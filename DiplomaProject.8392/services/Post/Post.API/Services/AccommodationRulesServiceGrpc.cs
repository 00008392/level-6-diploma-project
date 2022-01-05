using Grpc.Core;
using Post.API.Services.Strategies;
using Post.Domain.Entities;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post.API.Services
{
    public class AccommodationRulesServiceGrpc : AccommodationRules.AccommodationRulesBase
    {
        public AccommodationRulesServiceGrpc(IAccommodationItemsStrategy<AccommodationRule, Rule> strategy)
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
