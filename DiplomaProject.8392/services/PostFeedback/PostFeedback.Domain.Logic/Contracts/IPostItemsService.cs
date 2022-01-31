using PostFeedback.Domain.Entities;
using PostFeedback.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Contracts
{
    public interface IPostItemsService<T, E>
         where T : PostItem
         where E : Item
    {
        Task<ICollection<ItemInfoDTO>> GetItemsAsync();
        Task AddItemsAsync(long postId, ICollection<AddItemToPostDTO> items);
        Task RemoveItemsAsync(long postId, ICollection<long> itemsIds);
    }
}
