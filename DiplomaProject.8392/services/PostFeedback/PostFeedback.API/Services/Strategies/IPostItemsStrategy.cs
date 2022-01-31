using PostFeedback.Domain.Entities;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostFeedback.API.Services.Strategies
{
    public interface IPostItemsStrategy<T, E>
         where T : Domain.Entities.PostItem
         where E : Domain.Entities.Item
    {
        Task<ICollection<Item>> GetItemsAsync();
        Task<Response> AddItemsAsync(AddItemsRequest request);
        Task<Response> RemoveItemsAsync(RemoveItemsRequest request);
    }
}
