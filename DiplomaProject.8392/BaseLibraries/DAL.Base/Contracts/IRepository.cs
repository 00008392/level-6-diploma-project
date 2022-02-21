using DAL.Base.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Base.Contracts
{
    public interface IRepository<T> where T : BaseEntity
                                          
    {
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(long id);
        Task AddRangeAsync(ICollection<T> items);
        Task RemoveRangeAsync(ICollection<T> items);
        bool DoesItemWithIdExist(long id);
        Task<T> GetByIdAsync(long id, params Expression<Func<T, object>>[] includes);
        Task<ICollection<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<ICollection<T>> GetFilteredAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
    }
}
