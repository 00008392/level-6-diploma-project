using BaseClasses.Contracts;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Core
{
    public interface IPostRepository: IBaseRepository<Accommodation>
    {
        Task<Accommodation> GetByIdAsync(long id);
        Task<ICollection<Accommodation>> GetAllAsync();
        Task<ICollection<Accommodation>> GetFilteredAsync(Expression<Func<Accommodation, bool>> filter);
    }
}
