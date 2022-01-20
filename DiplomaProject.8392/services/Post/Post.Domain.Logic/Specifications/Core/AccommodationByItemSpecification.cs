using BaseClasses.Specifications;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.Specifications.Core
{
    public abstract class AccommodationByItemSpecification : Specification<Accommodation>
    {
        protected readonly long _itemId;
        protected AccommodationByItemSpecification(long itemId)
        {
            _itemId = itemId;
        }
    }
}
