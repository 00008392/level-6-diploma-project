using BaseClasses.Entities;
using Booking.Domain.Logic.DTOs.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.Contracts
{
    public interface IEventHandlerService<T>
        where T: BaseEntity
    {
        Task CreateEntityAsync(ICreateEntityDTO entityDTO);
        Task UpdateEntityAsync(IEntityDTO entityDTO);
        Task DeleteEntityAsync(long id);
    }
}
