using FrontEndApp.Models.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Services.Post.Contracts
{
    public interface IPostRelatedInfoService
    {
        Task<ICollection<Item>> GetCitiesAsync();
        Task<ICollection<Item>> GetCategoriesAsync();
        Task<ICollection<Item>> GetRulesAsync();
        Task<ICollection<Item>> GetFacilitiesAsync();
    }
}
