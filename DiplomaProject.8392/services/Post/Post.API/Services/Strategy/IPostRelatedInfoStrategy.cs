using Post.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post.API.Services.Strategy
{
    public interface IPostRelatedInfoStrategy<T, E> 
        where T: ItemAccommodationBase, new()
        where E: ItemBase
    {
        Task<Response> AddItemAsync(AddItemRequest request);
        Task<Response> RemoveItemAsync(Request request);
    }
}
