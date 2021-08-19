using BaseClasses.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BaseClasses.Contracts
{
    public interface IRepository<T> : IBaseRepository<T> where T : BaseEntity
                                                       
    {

        Task<T> GetByIdAsync(long id, params Expression<Func<T, object>>[] includes);
        Task<ICollection<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<ICollection<T>> GetFilteredAsync(Expression<Func<T, bool>> filter,
            params Expression<Func<T, object>>[] includes);

    }
}
