using Post.Domain.Core;
using Post.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.Contracts
{
    //get all rules/facilities/specificities
    public interface IPostItemsService<T>
        where T: ItemBase
    {
        Task<ICollection<ItemInfoDTO>> GetItemsAsync();
    }
}
