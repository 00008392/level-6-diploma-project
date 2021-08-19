using BaseClasses.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseClasses.Contracts
{
    public interface IBaseRepository<T> where T : BaseEntity
                                          
    {
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task AddRangeAsync(ICollection<T> items);
        Task RemoveRangeAsync(ICollection<T> items);
        bool DoesItemWithIdExist(long id);
    }
}
