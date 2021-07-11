using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Profile.Domain.Core
{
    public interface IRepository<T> where T: BaseEntity
    {
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> GetByIdAsync(long id);
        Task<ICollection<T>> GetAllAsync();
        Task<ICollection<T>> GetFilteredAsync(Expression<Func<T, bool>> filter);
        bool IfExists(long id);
    }
}
