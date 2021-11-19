using Post.Domain.Core;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post.API.Services.Strategies
{
    public interface IPostItemsStrategy<T>
        where T: ItemBase
    {
        Task<ICollection<Item>> GetItems();
    }
}
