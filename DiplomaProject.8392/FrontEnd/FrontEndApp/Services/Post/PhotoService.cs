using FrontEndApp.Models;
using FrontEndApp.Models.Post;
using FrontEndApp.Services.Post.Contracts;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FrontEndApp.Services.Post
{
    public class PhotoService : IPhotoService
    {
        private readonly HttpClient _client;

        public PhotoService(HttpClient client)
        {
            _client = client;
        }

        public async Task<Response> AddCoverPhotoAsync(IBrowserFile inputFile, long postId,
            Action onSuccessAction = null, Action onErrorAction = null)
        {
            var response = new Response();
            try
            {
                // setting the max size for the file 
                long maxFileSize = 1024 * 1024;
                var fileContent = new StreamContent(inputFile.OpenReadStream(maxFileSize));
                // read file content type
                var contentType = inputFile.ContentType;
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                 var content = new MultipartFormDataContent();
                content.Add
                (content: fileContent, name: "\"file\"", fileName: inputFile.Name);
                //call api
                var httpReply = await _client.PostAsync($"api/post-photos/cover/{postId}", content);
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

        public async Task<Response> AddPhotosAsync(ICollection<IBrowserFile> inputFiles, long postId,
            Action onSuccessAction = null, Action onErrorAction = null)
        {
            var response = new Response();
            try
            {
                var content = new MultipartFormDataContent();
                foreach (var inputFile in inputFiles)
                {
                    // setting the max size for the file 
                    long maxFileSize = 1024 * 1024;
                    var fileContent = new StreamContent(inputFile.OpenReadStream(maxFileSize));
                    // read file content type
                    var contentType = inputFile.ContentType;
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                    content.Add
                    (content: fileContent, name: "\"files\"", fileName: inputFile.Name);
                }
                //call api
                var httpReply = await _client.PostAsync($"api/post-photos/{postId}", content);
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
            catch(Exception ex)
            {
                response.IsSuccess = false;
            }
            return response;
        }

        public async Task<Photo> GetCoverPhotoAsync(long postId)
        {
            try
            {
                var reply = await _client.GetAsync($"api/post-photos/cover/{postId}");
                if (reply.IsSuccessStatusCode)
                {
                    var responseStr = await reply.Content.ReadAsStringAsync();
                    var photo = JsonConvert.DeserializeObject<Photo>(responseStr);
                    photo.FileContentStr = string.Format("data:image/gif;base64,{0}",
           Convert.ToBase64String(photo.FileContent));
                    return photo;
                }
            }
            catch
            {
            }
            return null;
        }

        public async Task<ICollection<Photo>> GetPhotosAsync(long postId)
        {
            try
            {
                var reply = await _client.GetAsync($"api/post-photos/post/{postId}");
                if (reply.IsSuccessStatusCode)
                {
                    var responseStr = await reply.Content.ReadAsStringAsync();
                    var photos = JsonConvert.DeserializeObject<List<Photo>>(responseStr);
                    photos.ForEach(x =>
                    {
                        x.FileContentStr = string.Format("data:image/gif;base64,{0}",
                            Convert.ToBase64String(x.FileContent));
                    });
                    return photos.Count == 0 ?null:photos;
                }
            }
            catch
            {
            }
            return null;
        }
        public async Task<Response> RemovePhotoAsync(long id, Action onSuccessAction = null,
          Action onErrorAction = null)
        {
            var response = new Response();
            try
            {
                //call api
                var httpReply = await _client.DeleteAsync($"api/post-photos/{id}");
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

    }
}
