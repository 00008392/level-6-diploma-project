using Grpc.Core;
using Post.API.Services.Strategies;
using Post.Domain.Core;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Post.API.ProtoClasses.Core
{
    public abstract class AccommodationItemsBase<T, E>
        where T : ItemAccommodationBase
        where E : ItemBase
    {
        protected readonly IAccommodationItemsStrategy<T, E> _strategy;

        protected AccommodationItemsBase(IAccommodationItemsStrategy<T, E> strategy)
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
