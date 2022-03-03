using APIGateway.Exceptions;
using APIGateway.Helpers;
using APIGateway.Services.Contracts;
using Google.Protobuf;
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
        [HttpGet("post/{postId}")]
        public async Task<IActionResult> GetPhotos(long postId)
        {
            //call grpc service
            var reply = await _photoClient.GetPhotosForPostAsync(new Request { Id = postId });
            //convert byte string to byte array
            foreach (var item in reply.Items)
            {
                item.FileContent = item.PhotoByteStr?.ToByteArray();
            }
            //return photos in byte array format
            return Ok(reply.Items);
        }
        //retrieve cover photo by post
        [HttpGet("cover/{postId}")]
        public async Task<IActionResult> GetCoverPhoto(long postId)
        {
            //call grpc service
            var reply = await _photoClient.GetCoverPhotoForPostAsync(new Request { Id = postId });
            if(reply.NoItem)
            {
                return NotFound("Photo not found");
            }
            //convert byte string to byte array
            reply.FileContent = reply.PhotoByteStr?.ToByteArray();
            //return photo in byte array format
            return Ok(reply);
        }
        //retrieve photo by id
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetPhoto(long id)
        {
            //call grpc service
            var reply = await _photoClient.GetPhotoAsync(new Request { Id = id });
            if (reply.NoItem)
            {
                return NotFound("Photo not found");
            }
            //convert byte string to byte array
            reply.FileContent = reply.PhotoByteStr?.ToByteArray();
            //return photo in byte array format
            return Ok(reply);
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
            var reply = new Response();
            var request = new AddPhotosRequest
            {
                PostId = postId
            };
            //try to transform each photo into stream of bytes and add photos to post
            try
            {
                //check that there are files
                if(files==null||files.Length==0)
                {
                    reply.Message = "No files submitted";
                    return BadRequest(reply);
                }
                //convert image file to byte array and prepare grpc request
                request = PreparePhotoCollectionRequest(files, request);
                //try to add photos
                reply = await _photoClient.AddPhotosToPostAsync(request);
                if (!reply.IsSuccess)
                {
                    //in case of errors, indicate it in response
                    return BadRequest(reply);
                }
                return StatusCode(201);
            }
            //catch exception if file is not an image and return bad request
            catch(FileContentTypeException ex)
            {
                reply.Message = ex.Message;
               return BadRequest(reply);
            }
        }
        //only authorized access
        //add cover photo to po9st
        [Authorize]
        [HttpPost("cover/{postId}")]
        public async Task<IActionResult> UploadCoverPhoto(IFormFile file,
            long postId)
        {
            //check if user is authorized to add cover photo
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, postId, "PostUpdatePolicy");
            if (!authorizationResult.Succeeded)
            {
                return Unauthorized();
            }
            var reply = new Response();
            var request = new Photo
            {
                PostId = postId
            };
            //try to transform photo into stream of bytes and add to post
            try
            {
                //check that there are files
                if (file == null)
                {
                    reply.Message = "No file submitted";
                    return BadRequest(reply);
                }
                //convert image file to byte array and prepare grpc request
                request.PhotoByteStr = PreparePhotoRequest(file);
                //try to add photo
                reply = await _photoClient.AddCoverPhotoToPostAsync(request);
                if (!reply.IsSuccess)
                {
                    //in case of errors, indicate it in response
                    return BadRequest(reply);
                }
                return StatusCode(201);
            }
            //catch exception if file is not an image and return bad request
            catch (FileContentTypeException ex)
            {
                reply.Message = ex.Message;
                return BadRequest(reply);
            }
        }

        //only authorized access
        //remove photo from post
        [Authorize]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePostPhoto(long id)
        {
            //check if user is authorized to remove photos
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, id, "PhotoDeletePolicy");
            if (!authorizationResult.Succeeded)
            {
                return Unauthorized();
            }
            //prepare grpc request
            var request = new Request
            {
                Id = id
            };
            //call grpc service to remove photos
            var reply = await _photoClient.RemovePhotoFromPostAsync(request);
            if (!reply.IsSuccess)
            {
                //in case of errors, indicate it in response
                return BadRequest(reply);
            }
            return NoContent();
        }
        //converts image file to byte array and prepares grpc request
        private AddPhotosRequest PreparePhotoCollectionRequest(
            IFormFile[] files,
            AddPhotosRequest request)
        {
            foreach (var file in files)
            {
                request.PhotoByteStr.Add(PreparePhotoRequest(file));
            }
            return request;
        }
        private ByteString PreparePhotoRequest(IFormFile file)
        {
            //convert file to image
            var bytes = _imageConverter.ConvertFile(file);
            //prepare grpc request
            return GrpcConversion.FromByteArrayToByteString(bytes);
        }

    }
}
