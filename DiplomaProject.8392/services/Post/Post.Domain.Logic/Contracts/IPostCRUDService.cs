using Post.Domain.Logic.DTOs;
using Post.Domain.Logic.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.Contracts
{
    public interface IPostCRUDService
    {
        Task CreatePostAsync(CreatePostDTO item);
        Task UpdatePostAsync(UpdatePostDTO item);
        Task DeletePostAsync(long id);
        Task<AccommodationInfoDTO> GetPostByIdAsync(long id);
        Task<ICollection<AccommodationInfoDTO>> GetAllPostsAsync(FilterParameters filter);
    }
}
