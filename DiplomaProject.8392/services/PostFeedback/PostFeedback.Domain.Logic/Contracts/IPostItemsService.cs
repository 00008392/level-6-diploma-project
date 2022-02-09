using PostFeedback.Domain.Entities;
using PostFeedback.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Contracts
{
    //post items - rules/facilities that accommodation can have 
    //this service is responsible for manipulation with these items 
    public interface IPostItemsService<T, E>
         where T : PostItem //bridge between post and item
         where E : Item
    {
        //Get all items 
        Task<ICollection<ItemInfoDTO>> GetItemsAsync();
        //add specific items to accommodation
        Task AddItemsAsync(long postId, ICollection<AddItemToPostDTO> items);
        //remove specific items that accommodation has
        Task RemoveItemsAsync(long postId, ICollection<long> itemsIds);
    }
}
