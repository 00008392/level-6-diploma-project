using DAL.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Logic.Base.Specifications
{
    public abstract class CompositeSpecification<T>: Specification<T> where T: BaseEntity
    {
        protected Specification<T> _left;
        protected Specification<T> _right;
        public CompositeSpecification(Specification<T> left, Specification<T> right)
        {
            _left = left;
            _right = right;
        }
    }
}

