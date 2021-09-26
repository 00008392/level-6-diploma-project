using BaseClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BaseClasses.Contracts
{
    public interface IRepository<T>: IBaseRepository<T> 
        where T: BaseEntity
    {
        Task<T> GetByIdAsync(long id);
        Task<ICollection<T>> GetAllAsync();
        Task<ICollection<T>> GetFilteredAsync(Expression<Func<T, bool>> filter);
    }
}
