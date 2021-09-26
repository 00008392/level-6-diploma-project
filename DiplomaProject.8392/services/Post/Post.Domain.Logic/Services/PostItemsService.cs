using AutoMapper;
using BaseClasses.Contracts;
using Post.Domain.Core;
using Post.Domain.Logic.Contracts;
using Post.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.Services
{
    public class PostItemsService<T> : IPostItemsService<T>
        where T : ItemBase
    {
        private readonly IRepository<T> _repository;
        private readonly IMapper _mapper;
        public PostItemsService(IRepository<T> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ICollection<ItemInfoDTO>> GetItemsAsync()
        {
           var items = await _repository.GetAllAsync();
            var itemDTOs = _mapper.Map<ICollection<T>, ICollection<ItemInfoDTO>>(items);
            return itemDTOs;
        }
    }
}
