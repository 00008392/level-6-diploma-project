using Post.Domain.Core;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post.API.Services.Strategies
{
    public interface IPostRelatedInfoStrategy<T, E> 
        where T: ItemAccommodationBase
        where E: ItemBase
    {
        Task<Response> AddItemsAsync(AddItemsRequest request);
        Task<Response> RemoveItemsAsync(RemoveItemsRequest request);
    }
}
