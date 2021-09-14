using Post.Domain.Core;
using Post.Domain.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post.API.Services.Strategies
{
    public class PostItemsGenericStrategy<T> : IPostItemsStrategy<T>
        where T: ItemBase
    {
        private readonly IPostItemsService<T> _service;
        public PostItemsGenericStrategy(IPostItemsService<T> service)
        {
            _service = service;
        }
        public async Task<ICollection<Item>> GetItems()
        {
            return (await _service.GetItemsAsync()).Select(x=>new Item { 
            Id = x.Id,
            Name = x.Name
            }).ToList();
        }
    }
}
