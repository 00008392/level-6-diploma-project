using AutoMapper;
using Post.Domain.Core;
using Post.Domain.Logic.Contracts;
using Post.Domain.Logic.DTOs;
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
        private readonly IMapper _mapper;
        public PostItemsGenericStrategy(IPostItemsService<T> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        public async Task<ICollection<Item>> GetItems()
        {

            return _mapper.Map<ICollection<ItemInfoDTO>, ICollection<Item>>
                (await _service.GetItemsAsync());
        }
    }
}
