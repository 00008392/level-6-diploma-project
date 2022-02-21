using PostFeedback.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Contracts
{
    public interface IPostPhotoService
    {
        Task AddPhotosToPost(long postId, ICollection<PhotoDTO> photos);
        Task RemovePhotosFromPost(long postId, ICollection<long> photos);
        Task<ICollection<PhotoDTO>> GetPhotosForPost(long postId);
    }
}
