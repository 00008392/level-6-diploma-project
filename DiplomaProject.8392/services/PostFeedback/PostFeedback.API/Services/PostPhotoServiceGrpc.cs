using AutoMapper;
using FluentValidation;
using Grpc.Base.Extensions;
using Grpc.Base.Helpers;
using Grpc.Core;
using PostFeedback.Domain.Logic.Contracts;
using PostFeedback.Domain.Logic.DTOs;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostFeedback.API.Services
{
    //grpc service for manipulation of photos attached to post
    public class PostPhotoServiceGrpc: PostPhotoService.PostPhotoServiceBase
    {
        //inject domain logic service
        private readonly IPostPhotoService _service;
        private readonly IMapper _mapper;

        public PostPhotoServiceGrpc(
            IPostPhotoService service,
            IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        //attach photos to post
        public override async Task<Response> AddPhotosToPost(AddPhotosRequest request,
            ServerCallContext context)
        {
            var response = new Response();
            //map grpc objects to dtos
            var photos = _mapper.Map<ICollection<PhotoDTO>>(request.Photos);
            try
            {
                //try to attach photos
                await _service.AddPhotosToPost(request.PostId, photos);
                //if successful, indicate it in response
                response.IsSuccess = true;
            }
            catch (ValidationException ex)
            {
                //if validation fails, add validation errors to the response
                response.HandleValidationException(ex);
            }
            catch (Exception ex)
            {
                //in case of other error, indicate it in response
                response.HandleException(ex);
            }
            return response;
        }
        //remove photos from post
        public override async Task<Response> RemovePhotosFromPost(RemovePhotosRequest request,
            ServerCallContext context)
        {
            var response = new Response();
            try
            {
                //try to remove photos
                await _service.RemovePhotosFromPost(request.PostId, request.Photos);
                //if successful, indicate it in response
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                //in case of other error, indicate it in response
                response.HandleException(ex);
            }
            return response;
        }
        //retrieve photos for post
        public override async Task<PhotoListResponse> GetPhotosForPost(Request request,
            ServerCallContext context)
        {
            //get photos
            var photos = await _service.GetPhotosForPost(request.Id);
            //map to response
            return GrpcServiceHelper.MapItems<PhotoListResponse, PhotoDTO,
                Photo>(_mapper, photos);
        }
    }
}
