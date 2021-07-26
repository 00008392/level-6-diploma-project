using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Core
{
    public interface IRepository<T> where T: BaseEntity
    {
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> GetByIdAsync(long id, params Expression<Func<T, object>>[] includes);
        Task<ICollection<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<ICollection<T>> GetFilteredAsync(Expression<Func<T, bool>> filter,
            params Expression<Func<T, object>>[] includes);
        bool IfExists(long id);
    }
}
