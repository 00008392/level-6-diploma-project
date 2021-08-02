using Grpc.Core;
using Post.API.Services.Strategies;
using Post.Domain.Core;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post.API.Services
{
    public class PostItemsServiceGrpc: Items.ItemsBase
    {
        private readonly IPostItemsStrategy<Rule> _ruleStrategy;
        private readonly IPostItemsStrategy<Facility> _facilityStrategy;
        private readonly IPostItemsStrategy<Specificity> _specificityStrategy;

        public PostItemsServiceGrpc(IPostItemsStrategy<Rule> ruleStrategy,
            IPostItemsStrategy<Facility> facilityStrategy,
            IPostItemsStrategy<Specificity> specificityStrategy)
        {
            _ruleStrategy = ruleStrategy;
            _facilityStrategy = facilityStrategy;
            _specificityStrategy = specificityStrategy;
        }
        public override async Task<ItemsList> GetRules(Empty request, ServerCallContext context)
        {
            return await GetItems(_ruleStrategy);
        }
        public override async Task<ItemsList> GetFacilities(Empty request, ServerCallContext context)
        {
            return await GetItems(_facilityStrategy);
        }
        public override async Task<ItemsList> GetSpecificities(Empty request, ServerCallContext context)
        {
            return await GetItems(_specificityStrategy);
        }

        private async Task<ItemsList> GetItems<T>(IPostItemsStrategy<T> strategy) where T: ItemBase
        {
            var list = new ItemsList();
            list.Items.Add(await strategy.GetItems());
            return list;
        }
    }
}
