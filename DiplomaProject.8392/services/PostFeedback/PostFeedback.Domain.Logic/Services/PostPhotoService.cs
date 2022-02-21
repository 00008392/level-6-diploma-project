using AutoMapper;
using DAL.Base.Contracts;
using Domain.Logic.Base.Exceptions;
using Domain.Logic.Base.Helpers;
using PostFeedback.Domain.Entities;
using PostFeedback.Domain.Logic.Contracts;
using PostFeedback.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Services
{
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

        public async Task AddPhotosToPost(long postId, ICollection<PhotoDTO> photoDTOs)
        {
            ServiceHelper.CheckIfRelatedEntityExists(postId, _postRepository);
            var photos = new List<Photo>();
            foreach(var photoDTO in photoDTOs)
            {
                var photo = new Photo(postId, photoDTO.Photo, photoDTO.MimeType);
                photos.Add(photo);
            }
           await _photoRepository.AddRangeAsync(photos);
        }

        public async Task<ICollection<PhotoDTO>> GetPhotosForPost(long postId)
        {
            var photos = await _photoRepository.GetFilteredAsync(x => x.PostId == postId);
            return _mapper.Map<ICollection<PhotoDTO>>(photos);
        }

        public async Task RemovePhotosFromPost(long postId, ICollection<long> photoIds)
        {
            ServiceHelper.CheckIfRelatedEntityExists(postId, _postRepository);
            var photos = new List<Photo>();
            foreach (var id in photoIds.Distinct())
            {
                var photo = await _photoRepository.GetByIdAsync(id);
                if(photo==null||photo.PostId!=postId)
                {
                    throw new NotFoundException(id, nameof(Photo));
                }
                photos.Add(photo);
            }
            await _photoRepository.RemoveRangeAsync(photos);
        }
    }
}
