using FrontEndApp.Models.Post;
using FrontEndApp.Services.Post.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontEndApp.Services.Post
{
    public class PostRelatedInfoService : IPostRelatedInfoService
    {
        private readonly HttpClient _client;

        public PostRelatedInfoService(HttpClient client)
        {
            _client = client;
        }
        public async Task<ICollection<Item>> GetCategoriesAsync()
        {
            try
            {
                var reply = await _client.GetAsync($"api/posts/info/categories");
                if (reply.IsSuccessStatusCode)
                {
                    var responseStr = await reply.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Item>>(responseStr);
                }
            }
            catch
            {
            }
            return null;
        }

        public async Task<ICollection<Item>> GetCitiesAsync()
        {
            try
            {
                var reply = await _client.GetAsync($"api/posts/info/cities");
                if (reply.IsSuccessStatusCode)
                {
                    var responseStr = await reply.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Item>>(responseStr);
                }
            }
            catch
            {
            }
            return null;
        }

        public async Task<ICollection<Item>> GetFacilitiesAsync()
        {
            try
            {
                var reply = await _client.GetAsync($"api/posts/info/facilities");
                if (reply.IsSuccessStatusCode)
                {
                    var responseStr = await reply.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Item>>(responseStr);
                }
            }
            catch
            {
            }
            return null;
        }

        public async Task<ICollection<Item>> GetRulesAsync()
        {
            try
            {
                var reply = await _client.GetAsync($"api/posts/info/rules");
                if (reply.IsSuccessStatusCode)
                {
                    var responseStr = await reply.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Item>>(responseStr);
                }
            }
            catch
            {
            }
            return null;
        }
    }
}
