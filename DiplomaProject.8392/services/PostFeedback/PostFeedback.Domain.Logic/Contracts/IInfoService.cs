using PostFeedback.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Contracts
{
    public interface IInfoService
    {
        Task<ICollection<CategoryCityDTO>> GetAllCitiesAsync();
        Task<ICollection<CategoryCityDTO>> GetAllCategoriesAsync();
    }
}
