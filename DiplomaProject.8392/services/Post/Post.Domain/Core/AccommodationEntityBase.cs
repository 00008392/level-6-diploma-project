using BaseClasses.Entities;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Core
{
   public abstract class AccommodationEntityBase: BaseEntity
    {
        public long AccommodationId { get; protected set; }
        public Accommodation Accommodation { get; }

        protected AccommodationEntityBase(long accommodationId)
        {
            AccommodationId = accommodationId;
        }
        protected AccommodationEntityBase()
        {
        
        }
    }

}
