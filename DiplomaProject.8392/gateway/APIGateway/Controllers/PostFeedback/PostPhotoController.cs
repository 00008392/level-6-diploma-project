using APIGateway.Exceptions;
using APIGateway.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostFeedback.API;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIGateway.Controllers.PostFeedback
{
    //controller for manipulation of photos attached to post
    [Route("api/post-photos")]
    [ApiController]
    public class PostPhotoController : ControllerBase
    {
        //inject grpc client to access services of post/feedback microservice
        private readonly PostPhotoService.PostPhotoServiceClient _photoClient;
        //injecting authorization service for resource based authorization
        private readonly IAuthorizationService _authorizationService;
        //inject file converter to convert image files into bytes
        private readonly IFileConverter _imageConverter;

        public PostPhotoController(
            PostPhotoService.PostPhotoServiceClient photoClient,
            IAuthorizationService authorizationService,
            IFileConverter imageConverter)
        {
            _photoClient = photoClient;
            _authorizationService = authorizationService;
            _imageConverter = imageConverter;
        }

        //retrieve photos by post
        [HttpGet("{postId}")]
        public async Task<IActionResult> GetPhotos(long postId)
        {
            //call grpc service
            var reply = await _photoClient.GetPhotosForPostAsync(new Request { Id = postId });
            //convert byte string to byte array
            foreach (var item in reply.Items)
            {
                item.FileContent = item.PhotoByteStr.ToByteArray();
            }
            //return photos in byte array format
            return Ok(reply.Items);
        }
        //only authorized access
        //attach photos to post
        [Authorize]
        [HttpPost("{postId}")]
        public async Task<IActionResult> UploadPostPhotos(IFormFile[] files,
            long postId)
        {
            //check if user is authorized to add photos
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, postId, "PostUpdatePolicy");
            if (!authorizationResult.Succeeded)
            {
                return Unauthorized();
            }
            var request = new AddPhotosRequest
            {
                PostId = postId
            };
            //try to transform each photo into stream of bytes and add photos to post
            try
            {
                foreach (var file in files)
                {
                    //convert file to image
                    var bytes = _imageConverter.ConvertFile(file);
                    //prepare grpc request
                    var photo = new Photo
                    {
                        PhotoByteStr = Google.Protobuf.ByteString.CopyFrom(bytes)
                    };
                    request.Photos.Add(photo);
                }
                //try to add photos
                var reply = await _photoClient.AddPhotosToPostAsync(request);
                if (!reply.IsSuccess)
                {
                    //in case of errors, indicate it in response
                    return BadRequest(reply);
                }
                return StatusCode(201);
            }
            //catch exception if file is empty or not an image and return bad request
            catch(Exception ex)
            {
                if (ex is EmptyFileException || ex is FileContentTypeException)
                {
                    return BadRequest(ex.Message);
                }
                throw;
            }
        }
        //only authorized access
        //remove photos from post
        [Authorize]
        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeletePostPhotos(ICollection<long> photos,
            long postId)
        {
            //check if user is authorized to remove photos
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, postId, "PostUpdatePolicy");
            if (!authorizationResult.Succeeded)
            {
                return Unauthorized();
            }
            //prepare grpc request
            var request = new RemovePhotosRequest
            {
                PostId = postId
            };
            request.Photos.AddRange(photos);
            //call grpc service to remove photos
            var reply = await _photoClient.RemovePhotosFromPostAsync(request);
            if (!reply.IsSuccess)
            {
                //in case of errors, indicate it in response
                return BadRequest(reply);
            }
            return NoContent();
        }
    }
}
