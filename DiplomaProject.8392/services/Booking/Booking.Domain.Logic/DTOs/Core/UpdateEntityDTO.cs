using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.DTOs.Core
{
    public abstract class UpdateEntityDTO
    {
         public long Id { get; protected set; }

        protected UpdateEntityDTO(long id)
        {
            Id = id;
        }
    }
}
