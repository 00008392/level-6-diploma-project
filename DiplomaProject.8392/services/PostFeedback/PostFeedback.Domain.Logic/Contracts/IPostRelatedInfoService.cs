using PostFeedback.Domain.Entities;
using PostFeedback.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Contracts
{
    //service for retrieving entities related to post and necessary for post creation and modification
    //in this case, such entities are city, category, rule, facility
    public interface IPostRelatedInfoService<T> where T: Item
    {
        Task<ICollection<ItemDTO>> GetAllItemsAsync();   
    }
}
