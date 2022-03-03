using AutoMapper;
using DAL.Base.Contracts;
using Domain.Logic.Base.Exceptions;
using Domain.Logic.Base.Helpers;
using FluentValidation;
using PostFeedback.Domain.Entities;
using PostFeedback.Domain.Logic.Contracts;
using PostFeedback.Domain.Logic.DTOs;
using PostFeedback.Domain.Logic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Services
{
    //service that manipulates photos of accommodation attached to post
    public class PostPhotoService : IPostPhotoService
    {
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<Photo> _photoRepository;
        private readonly IMapper _mapper;
        private readonly AbstractValidator<PhotoDTO> _validator;

        public PostPhotoService(
            IRepository<Post> postRepository,
            IRepository<Photo> photoRepository,
            IMapper mapper,
            AbstractValidator<PhotoDTO> validator)
        {
            _postRepository = postRepository;
            _photoRepository = photoRepository;
            _mapper = mapper;
            _validator = validator;
        }
        //add main photo to post
        public async Task AddCoverPhotoToPostAsync(PhotoDTO photoDTO)
        {
            //validate dto
            ServiceHelper.ValidateItem(_validator, photoDTO);
            //check if cover photo already exists in the DB
            var existingPhoto = await GetCoverPhotoAsync(photoDTO.PostId);
            //if exists, remove it
            if(existingPhoto != null)
            {
                await _photoRepository.DeleteAsync(existingPhoto.Id);
            }
            //map dto to entity
            var photo = new Photo(photoDTO.PostId, photoDTO.Photo, isCover: true);
            //add new cover photo
            await _photoRepository.CreateAsync(photo);
        }

        //attach photos to post
        public async Task AddPhotosToPostAsync(long postId, ICollection<PhotoDTO> photoDTOs)
        {
            //check if post exists in the DB
            ServiceHelper.CheckIfRelatedEntityExists(postId, _postRepository);
            //check that limit of photos that post can have is not exceeded
            //get existing photos for post
            var existingPhotos = await _photoRepository.GetFilteredAsync(x => x.PostId == postId&&x.IsCover!=true);
            //check that sum of existing photos and added photos does not exceed 15
            if (existingPhotos?.Count + photoDTOs.Count > 15)
            {
                throw new NumberOfPhotosExceededException();
            }
            var photos = new List<Photo>();
            //map each dto to photo entity
            foreach(var photoDTO in photoDTOs)
            {
                var photo = new Photo(postId, photoDTO.Photo, isCover:false);
                photos.Add(photo);
            }
            //insert collection of photos to the DB
           await _photoRepository.AddRangeAsync(photos);
        }
        //retrieve cover photo for post
        public async Task<PhotoDTO> GetCoverPhotoForPostAsync(long postId)
        {
            //get photo from DB
            var photo = await GetCoverPhotoAsync(postId);
            //map entity to dto
            var photoDTO = _mapper.Map<PhotoDTO>(photo);
            return photoDTO;
        }
        //retrieve photo by id
        public async Task<PhotoDTO> GetPhotoAsync(long photoId)
        {
            //get photo from DB
            var photo = await _photoRepository.GetByIdAsync(photoId);
            //map entity to dto
            var photoDTO = _mapper.Map<PhotoDTO>(photo);
            return photoDTO;
        }

        //retrieve photos attached to post
        public async Task<ICollection<PhotoDTO>> GetPhotosForPostAsync(long postId)
        {
            //get all photos with indicated id of post except cover
            var photos = await _photoRepository.GetFilteredAsync(x => x.PostId == postId
            &&x.IsCover!=true);
            //map collection of photos to dto
            return _mapper.Map<ICollection<PhotoDTO>>(photos);
        }
        //remove photos attached to post
        public async Task RemovePhotoFromPostAsync(long photoId)
        {
            //ensure that photo exists in the DB
            var photo = await _photoRepository.GetByIdAsync(photoId);
            if(!_photoRepository.DoesItemWithIdExist(photoId))
            {
                throw new NotFoundException(photoId, nameof(Photo));
            }
            //delete photo
            await _photoRepository.DeleteAsync(photoId);
        }
        //retrieve cover photo by post id
        private async Task<Photo> GetCoverPhotoAsync(long postId)
        {
            return (await _photoRepository
                .GetFilteredAsync(x => x.PostId == postId && x.IsCover == true)).FirstOrDefault();
        }
    }
}
