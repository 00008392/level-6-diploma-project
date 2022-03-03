using FrontEndApp.Models;
using FrontEndApp.Models.Post;
using Microsoft.AspNetCore.Components.Forms;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Services.Post.Contracts
{
    public interface IPhotoService
    {
        Task<Response> AddPhotosAsync(ICollection<IBrowserFile> inputFiles, long postId,
            Action onSuccessAction = null, Action onErrorAction = null);
        Task<Response> RemovePhotoAsync(long id, Action onSuccessAction = null, Action onErrorAction = null);
        Task<Response> AddCoverPhotoAsync(IBrowserFile inputFile, long postId,
            Action onSuccessAction = null, Action onErrorAction = null);
        Task<ICollection<Photo>> GetPhotosAsync(long postId);
        Task<Photo> GetCoverPhotoAsync(long postId);
    }
}
