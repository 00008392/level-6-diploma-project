using Grpc.Core;
using PostFeedback.API.Services.Strategies;
using PostFeedback.Domain.Entities;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PostFeedback.API.ProtoClasses.Core
{
    public abstract class PostItemsBase<T, E>
        where T : Domain.Entities.PostItem
        where E : Domain.Entities.Item
    {
        protected readonly IPostItemsStrategy<T, E> _strategy;

        protected PostItemsBase(IPostItemsStrategy<T, E> strategy)
        {
            _strategy = strategy;
        }

        protected async Task<ItemsList> GetItems()
        {
            var list = new ItemsList();
            list.Items.Add(await _strategy.GetItemsAsync());
            return list;
        }
    }
}
