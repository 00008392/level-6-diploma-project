using PostFeedback.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Contracts
{
    //service that manipulates photos of accommodation attached to post
    public interface IPostPhotoService
    {
        Task AddPhotosToPostAsync(long postId, ICollection<PhotoDTO> photos);
        Task AddCoverPhotoToPostAsync(PhotoDTO photo);
        Task RemovePhotoFromPostAsync(long photoId);
        Task<PhotoDTO> GetPhotoAsync(long photoId);
        Task<ICollection<PhotoDTO>> GetPhotosForPostAsync(long postId);
        Task<PhotoDTO> GetCoverPhotoForPostAsync(long postId);
    }
}
