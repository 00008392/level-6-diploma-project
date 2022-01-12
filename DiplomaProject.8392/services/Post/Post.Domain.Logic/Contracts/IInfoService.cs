using Post.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.Contracts
{
    public interface IInfoService
    {
        Task<ICollection<CityDTO>> GetAllCitiesAsync();
        Task<ICollection<CategoryDTO>> GetAllCategoriesAsync();
    }
}
