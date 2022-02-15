using BaseClasses.Specifications;
using PostFeedback.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Specifications.Core
{
    public abstract class MinMaxSpecification : Specification<Post>
    {
        //base class for specifications that filter posts by range
        protected readonly int? _minNumber;
        protected readonly int? _maxNumber;

        public MinMaxSpecification(int? minNumber = null, int? maxNumber = null)
        {
            _minNumber = minNumber;
            _maxNumber = maxNumber;
        }
    }
}
