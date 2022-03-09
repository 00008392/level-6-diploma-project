using FrontEndApp.Models;
using FrontEndApp.Models.Post;
using FrontEndApp.Services.Core;
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
    //service that consumes post photo api
    public class PhotoService : BaseService, IPhotoService
    {
        public PhotoService(HttpClient client)
            :base(client)
        {
        }
        //add cover photo to post
        public async Task<Response> AddCoverPhotoAsync(IBrowserFile inputFile, long postId,
            Action onSuccessAction = null, Action onErrorAction = null)
        {
            //call base method
            return await HandleActionAsync(() =>
            {
                var content = new MultipartFormDataContent();
                //add prepared file to form content
                content.Add
                (content: PrepareImageContent(inputFile), name: "\"file\"", fileName: inputFile.Name);
                //call api
                return _client.PostAsync($"api/post-photos/cover/{postId}", content);
            }, onSuccessAction, onErrorAction);
        }
        //add multiple photos to post
        public async Task<Response> AddPhotosAsync(ICollection<IBrowserFile> inputFiles, long postId,
            Action onSuccessAction = null, Action onErrorAction = null)
        {
            //call base method
            return await HandleActionAsync(() =>
            {
                var content = new MultipartFormDataContent();
                //add prepared files to form content
                foreach (var inputFile in inputFiles)
                {
                    content.Add
                    (content: PrepareImageContent(inputFile), name: "\"files\"", fileName: inputFile.Name);
                }
                //call api
                return _client.PostAsync($"api/post-photos/{postId}", content);
            }, onSuccessAction, onErrorAction);
        }
        //retrieve cover photo
        public async Task<Photo> GetCoverPhotoAsync(long postId)
        {
            try
            {
                //call api
                var reply = await _client.GetAsync($"api/post-photos/cover/{postId}");
                if (reply.IsSuccessStatusCode)
                {
                    //in case of success, parse it and convert photo byte array to string
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
            //in case of error response or exception return null
            return null;
        }
        //retrieve multiple photos for post
        public async Task<ICollection<Photo>> GetPhotosAsync(long postId)
        {
            try
            {
                //call api
                var reply = await _client.GetAsync($"api/post-photos/post/{postId}");
                if (reply.IsSuccessStatusCode)
                {
                    //in case of success, parse it and convert photo byte array to string for each photo in collection
                    var responseStr = await reply.Content.ReadAsStringAsync();
                    var photos = JsonConvert.DeserializeObject<List<Photo>>(responseStr);
                    photos.ForEach(x =>
                    {
                        x.FileContentStr = string.Format("data:image/gif;base64,{0}",
                            Convert.ToBase64String(x.FileContent));
                    });
                    //if collection is empty, return null
                    return photos?.Count == 0 ?null:photos;
                }
            }
            catch
            {
            }
            //in case of error response or exception return null
            return null;
        }
        //delete photo
        public async Task<Response> RemovePhotoAsync(long id, Action onSuccessAction = null,
          Action onErrorAction = null)
        {
            //call base service method for delete
            return await HandleDeleteActionAsync($"api/post-photos/{id}", onSuccessAction, onErrorAction);
        }
        //method that prepares file content for multipart form data
        private StreamContent PrepareImageContent(IBrowserFile file)
        {
            // set the max size for the file (1 MB)
            long maxFileSize = 1024 * 1024;
            var fileContent = new StreamContent(file.OpenReadStream(maxFileSize));
            // read file content type
            var contentType = file.ContentType;
            //set content type
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            return fileContent;
        }
    }
}
