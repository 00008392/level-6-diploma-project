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
    public interface IRepositoryWithIncludes<T> : IBaseRepository<T> where T : BaseEntity
                                                       
    {

        Task<T> GetByIdAsync(long id, bool relatedEntitiesIncluded = false);
        Task<ICollection<T>> GetAllAsync(bool relatedEntitiesIncluded = false);
        Task<ICollection<T>> GetFilteredAsync(Expression<Func<T, bool>> filter,
            bool relatedEntitiesIncluded = false);

    }
}
