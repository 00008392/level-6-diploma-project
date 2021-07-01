using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Core
{
    public interface IRepository<T> where T: BaseEntity
    {
        Task CreateAsync(T entity);
        Task<List<T>> GetItemsAsync(Expression<Func<T, bool>> filter);
    }
}
