using FrontEndApp.Models;
using FrontEndApp.Models.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Services.Post.Contracts
{
    //service that consumes post api
    public interface IPostService
    {
        Task<Response> CreatePostAsync(EditPost post, Action onSuccessAction = null,
            Action onErrorAction = null);
        Task<Response> UpdatePostAsync(EditPost post, Action onSuccessAction = null,
            Action onErrorAction = null);
        Task<Response> DeletePostAsync(long id, Action onSuccessAction = null,
            Action onErrorAction = null);
        Task<PostResponse> GetPostAsync(long id, Action onNotFoundAction = null);
        Task<ICollection<PostResponse>> GetPostsAsync(Filter filter=null);
    }
}
