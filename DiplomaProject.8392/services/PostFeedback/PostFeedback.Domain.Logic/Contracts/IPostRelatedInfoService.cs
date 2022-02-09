using PostFeedback.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Contracts
{
    //service for retrieving entities related to post and necessary for post creation and modification
    //in this case, such entities are city and category
    public interface IPostrelatedInfoService
    {
        Task<ICollection<CategoryCityDTO>> GetAllCitiesAsync();
        Task<ICollection<CategoryCityDTO>> GetAllCategoriesAsync();
    }
}
