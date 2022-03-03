using AutoMapper;
using FluentValidation;
using Google.Protobuf;
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
            ICollection<PhotoDTO> photos = new List<PhotoDTO>();
            request.PhotoByteStr.ToList().ForEach(x => photos.Add(new PhotoDTO(
                x?.ToByteArray())));
            try
            {
                //try to attach photos
                await _service.AddPhotosToPostAsync(request.PostId, photos);
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
        //add cover photo to post
        public override async Task<Response> AddCoverPhotoToPost(Photo request,
            ServerCallContext context)
        {
            return await GrpcServiceHelper.HandleCreateUpdateActionAsync<PhotoDTO,
                Response, Photo>(_service.AddCoverPhotoToPostAsync, _mapper, request); 
        }
        //remove photo 
        public override async Task<Response> RemovePhotoFromPost(Request request,
            ServerCallContext context)
        {
            return await GrpcServiceHelper.HandleDeleteActionAsync<Response>(
                request.Id, _service.RemovePhotoFromPostAsync);
        }
        //retrieve photos for post
        public override async Task<PhotoListResponse> GetPhotosForPost(Request request,
            ServerCallContext context)
        {
            //get photos
            var photos = await _service.GetPhotosForPostAsync(request.Id);
            //map to response
            return GrpcServiceHelper.MapItems<PhotoListResponse, PhotoDTO,
                Photo>(_mapper, photos);
        }
        //retrieve cover photo for post
        public override async Task<Photo> GetCoverPhotoForPost(Request request,
            ServerCallContext context)
        {
            return await GrpcServiceHelper.HandleRetrievalByIdAsync<Photo, PhotoDTO>(request.Id,
                _service.GetCoverPhotoForPostAsync, _mapper);
        }
        //retrieve photo by id
        public override async Task<Photo> GetPhoto(Request request,
            ServerCallContext context)
        {
            return await GrpcServiceHelper.HandleRetrievalByIdAsync<Photo, PhotoDTO>(request.Id,
                _service.GetPhotoAsync, _mapper);
        }
    }
}
