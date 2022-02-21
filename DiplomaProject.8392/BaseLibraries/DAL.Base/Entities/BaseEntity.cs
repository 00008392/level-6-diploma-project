using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Base.Entities
{
    public abstract class BaseEntity
    {
        public long Id { get; protected set; }

        protected BaseEntity()
        {

        }
        protected BaseEntity(long id)
        {
            Id = id;
        }
    }
}
