using FrontEndApp.Models;
using FrontEndApp.Models.Post;
using FrontEndApp.Services.Post.Contracts;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FrontEndApp.Services.Post
{
    public class PostService : IPostService
    {
        private readonly HttpClient _client;

        public PostService(HttpClient client)
        {
            _client = client;
        }

        public async Task<Response> CreatePostAsync(EditPost post, Action onSuccessAction = null,
            Action onErrorAction = null)
        {
            var response = new Response();
            try
            {
                //call api
                var httpReply = await _client.PostAsJsonAsync("api/posts", post);
                if (httpReply.IsSuccessStatusCode)
                {
                    response.IsSuccess = true;
                    onSuccessAction?.Invoke();
                }
                //in case of error, parse error 
                else
                {
                    response.IsSuccess = false;
                    var errorMessage = httpReply.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<Response>(errorMessage);
                    onErrorAction?.Invoke();
                }
            }
            catch
            {
                response.IsSuccess = false;
            }
            return response;
        }

        public async Task<Response> DeletePostAsync(long id, Action onSuccessAction = null,
            Action onErrorAction = null)
        {
            var response = new Response();
            try
            {
                //call api
                var httpReply = await _client.DeleteAsync($"api/posts/{id}");
                if (httpReply.IsSuccessStatusCode)
                {
                    response.IsSuccess = true;
                    onSuccessAction?.Invoke();
                }
                else
                {
                    //in case of error, parse error 
                    response.IsSuccess = false;
                    var errorMessage = httpReply.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<Response>(errorMessage);
                    onErrorAction?.Invoke();
                }
            }
            catch
            {
                response.IsSuccess = false;
            }
            return response;
        }

        public async Task<PostResponse> GetPostAsync(long id, Action onNotFoundAction = null)
        {
            try
            {
                var reply = await _client.GetAsync($"api/posts/{id}");
                var post = new PostResponse();
                if (reply.IsSuccessStatusCode)
                {
                    var responseStr = await reply.Content.ReadAsStringAsync();
                    post = JsonConvert.DeserializeObject<PostResponse>(responseStr);
                } else
                {
                    post.NoItem = true;
                    onNotFoundAction?.Invoke();
                }
                return post;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ICollection<PostResponse>> GetPostsAsync(Filter filter=null)
        {
            //construct query string by placing each property with non null value to dictionary
            var queryParams = new Dictionary<string, string>();
            if(filter!=null)
            {
                foreach (var property in filter.GetType().GetProperties())
                {
                    var value = property.GetValue(filter);
                    if (value != null)
                    {
                        queryParams.Add(property.Name, value.ToString());
                    }
                }
            }
            var url = new Uri(QueryHelpers.AddQueryString
                ($"{_client.BaseAddress}api/posts", queryParams));
            //call api
            try
            {
                var response = await _client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseStr = await response.Content.ReadAsStringAsync();
                    var posts = JsonConvert.DeserializeObject<List<PostResponse>>(responseStr);
                    return posts.Count == 0 ? null : posts;
                }
            }
            catch
            {
            }
            return null;
        }
        public async Task<Response> UpdatePostAsync(EditPost post, Action onSuccessAction = null,
            Action onErrorAction = null)
        {
            var response = new Response();
            try
            {
                //call api
                var httpReply = await _client.PutAsJsonAsync($"api/posts", post);
                if (httpReply.IsSuccessStatusCode)
                {
                    response.IsSuccess = true;
                    onSuccessAction?.Invoke()
;                }
                //in case of error, parse error 
                else
                {
                    response.IsSuccess = false;
                    var errorMessage = httpReply.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<Response>(errorMessage);
                    onErrorAction?.Invoke();
                }
            }
            catch
            {
                response.IsSuccess = false;
            }
            return response;
        }
    }
}
