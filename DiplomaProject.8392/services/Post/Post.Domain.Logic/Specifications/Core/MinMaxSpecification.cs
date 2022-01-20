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
    public abstract class MinMaxSpecification : Specification<Accommodation>
    {
        protected readonly int? _minNumber;
        protected readonly int? _maxNumber;

        public MinMaxSpecification(int? minNumber = null, int? maxNumber = null)
        {
            _minNumber = minNumber;
            _maxNumber = maxNumber;
        }
    }
}
