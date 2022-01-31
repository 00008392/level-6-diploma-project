using PostFeedback.Domain.Logic.DTOs;
using PostFeedback.Domain.Logic.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Contracts
{
    public interface IPostCRUDService
    {
        Task CreatePostAsync(PostManipulationDTO item);
        Task UpdatePostAsync(PostManipulationDTO item);
        Task DeletePostAsync(long id);
        Task<PostDetailsDTO> GetPostByIdAsync(long id);
        Task<ICollection<PostDetailsDTO>> GetAllPostsAsync(FilterParameters filter);
    }
}
