using FrontEndApp.Models;
using FrontEndApp.Models.Post;
using FrontEndApp.Services.Core;
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
    //service that consumes post api
    public class PostService : BaseService, IPostService
    {
        public PostService(HttpClient client)
            :base(client)
        {
        }
        //create new post
        public async Task<Response> CreatePostAsync(EditPost post, Action onSuccessAction = null,
            Action onErrorAction = null)
        {
            //call base service method for create
            return await HandleCreateActionAsync(post, "api/posts", onSuccessAction, onErrorAction);
        }
        //delete post
        public async Task<Response> DeletePostAsync(long id, Action onSuccessAction = null,
            Action onErrorAction = null)
        {
            //call base service method for delete
            return await HandleDeleteActionAsync($"api/posts/{id}", onSuccessAction, onErrorAction);
        }
        //get post by id
        public async Task<PostResponse> GetPostAsync(long id, Action onNotFoundAction = null)
        {
            //call base service method for single item retrieval
            return await HandleSingleItemRetrievalAsync<PostResponse>($"api/posts/{id}", onNotFoundAction);
        }
        //get posts based on filter criteria
        public async Task<ICollection<PostResponse>> GetPostsAsync(Filter filter=null)
        {
            //create url with built query string
            var url = new Uri(QueryHelpers.AddQueryString
                ($"{_client.BaseAddress}api/posts", ConstructQueryString(filter)));
            //call base service method for multiple items retrieval
            return await HandleMultipleItemsRetrievalAsync<PostResponse>(url.ToString());
        }
        //update post
        public async Task<Response> UpdatePostAsync(EditPost post, Action onSuccessAction = null,
            Action onErrorAction = null)
        {
            //call base service method for update
            return await HandleUpdateActionAsync(post, "api/posts", onSuccessAction, onErrorAction);
        }
        //method that builds query string based on filter criteria
        private Dictionary<string, string> ConstructQueryString(Filter filter=null)
        {
            //construct query string by placing each property with non null value to dictionary
            var queryParams = new Dictionary<string, string>();
            if (filter != null)
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
            return queryParams;
        }
    }
}
