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
        public PostItemsService(IRepository<T> repository)
        {
            _repository = repository;
        }
        public async Task<ICollection<ItemInfoDTO>> GetItemsAsync()
        {
           var items = await _repository.GetAllAsync();
            var itemDTOs = new List<ItemInfoDTO>();
            foreach(var item in items)
            {

                var itemDTO = new ItemInfoDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    IsOther = item.IsOther
                };
                itemDTOs.Add(itemDTO);
            }
            return itemDTOs;
        }
    }
}
