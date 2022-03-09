using FrontEndApp.Models.Post;
using FrontEndApp.Services.Core;
using FrontEndApp.Services.Post.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontEndApp.Services.Post
{
    //service that consumes post related information api
    public class PostRelatedInfoService : BaseService, IPostRelatedInfoService
    {
        public PostRelatedInfoService(HttpClient client)
            :base(client)
        {
        }
        //get all categories
        public async Task<ICollection<Item>> GetCategoriesAsync()
        {
            //call base service method for multiple items retrieval
            return await HandleMultipleItemsRetrievalAsync<Item>("api/posts/info/categories");
        }
        //get all cities
        public async Task<ICollection<Item>> GetCitiesAsync()
        {
            //call base service method for multiple items retrieval
            return await HandleMultipleItemsRetrievalAsync<Item>("api/posts/info/cities");
        }
        //get all facilities
        public async Task<ICollection<Item>> GetFacilitiesAsync()
        {
            //call base service method for multiple items retrieval
            return await HandleMultipleItemsRetrievalAsync<Item>("api/posts/info/facilities");
        }
        //get all rules
        public async Task<ICollection<Item>> GetRulesAsync()
        {
            //call base service method for multiple items retrieval
            return await HandleMultipleItemsRetrievalAsync<Item>("api/posts/info/rules");
        }
    }
}
