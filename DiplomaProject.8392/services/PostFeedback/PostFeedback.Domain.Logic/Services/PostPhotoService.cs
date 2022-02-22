using AutoMapper;
using DAL.Base.Contracts;
using Domain.Logic.Base.Exceptions;
using Domain.Logic.Base.Helpers;
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

        public PostPhotoService(
            IRepository<Post> postRepository,
            IRepository<Photo> photoRepository,
            IMapper mapper)
        {
            _postRepository = postRepository;
            _photoRepository = photoRepository;
            _mapper = mapper;
        }
        //attach photos to post
        public async Task AddPhotosToPost(long postId, ICollection<PhotoDTO> photoDTOs)
        {
            //check if post exists in the DB
            ServiceHelper.CheckIfRelatedEntityExists(postId, _postRepository);
            var photos = new List<Photo>();
            //map each dto to photo entity
            foreach(var photoDTO in photoDTOs)
            {
                var photo = new Photo(postId, photoDTO.Photo);
                photos.Add(photo);
            }
            //insert collection of photos to the DB
           await _photoRepository.AddRangeAsync(photos);
        }
        //retrieve photos attached to post
        public async Task<ICollection<PhotoDTO>> GetPhotosForPost(long postId)
        {
            //get all photos with indicated id of post
            var photos = await _photoRepository.GetFilteredAsync(x => x.PostId == postId);
            //map collection of photos to dto
            return _mapper.Map<ICollection<PhotoDTO>>(photos);
        }
        //remove photos attached to post
        public async Task RemovePhotosFromPost(long postId, ICollection<long> photoIds)
        {
            //check if post exists in the DB
            ServiceHelper.CheckIfRelatedEntityExists(postId, _postRepository);
            var photos = new List<Photo>();
            //retrieve photos by ids, ensure that they exist in the DB 
            //and that they belong to indicated post
            foreach (var id in photoIds.Distinct())
            {
                var photo = await _photoRepository.GetByIdAsync(id);
                if(photo==null||photo.PostId!=postId)
                {
                    throw new DeletePhotoException(id);
                }
                photos.Add(photo);
            }
            //delete collection of items from the db
            await _photoRepository.RemoveRangeAsync(photos);
        }
    }
}
