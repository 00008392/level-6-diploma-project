using Post.Domain.Core;
using Post.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.Contracts
{
    public interface IAcommodationItemsService<T, E>
         where T : ItemAccommodationBase
         where E : ItemBase
    {
        Task<ICollection<ItemInfoDTO>> GetItemsAsync();
        Task AddItemsAsync(AddItemsDTO itemsDTO);
        Task RemoveItemsAsync(RemoveItemsDTO itemsDTOs);
    }
}
