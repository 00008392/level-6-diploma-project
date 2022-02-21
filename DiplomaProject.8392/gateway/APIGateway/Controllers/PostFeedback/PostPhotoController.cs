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
    [Route("api/post-photos")]
    [ApiController]
    public class PostPhotoController : ControllerBase
    {
        private readonly PostPhotoService.PostPhotoServiceClient _photoClient;

        public PostPhotoController(PostPhotoService.PostPhotoServiceClient photoClient)
        {
            _photoClient = photoClient;
        }
        [HttpGet("{postId}")]
        public async Task<IActionResult> GetPhotos(long postId)
        {
            var reply = await _photoClient.GetPhotosForPostAsync(new Request { Id = postId });
            foreach (var item in reply.Items)
            {
                item.FileContent = item.PhotoByteStr.ToByteArray();
            }
            return Ok(reply.Items);
        }
        [HttpPost("{postId}")]
        public async Task<IActionResult> UploadPostPhotos(IFormFile[] files,
            long postId)
        {
            var request = new AddPhotosRequest
            {
                PostId = postId
            };
            foreach (var file in files)
            {
                byte[] photoBytes = null;
                if (file != null)
                {
                    using (var memory = new MemoryStream())
                    {
                        file.CopyTo(memory);
                        photoBytes = memory.ToArray();
                    }
                }
                var photo = new Photo
                {
                    MimeType = file.ContentType,
                    PhotoByteStr = Google.Protobuf.ByteString.CopyFrom(photoBytes)
                };
                request.Photos.Add(photo);
            }
            var reply = await _photoClient.AddPhotosToPostAsync(request);
            if(!reply.IsSuccess)
            {
                return BadRequest(reply);
            }
            return StatusCode(201);
        }
    }
}
