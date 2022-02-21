using DAL.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Logic.Base.Extensions;

namespace Domain.Logic.Base.Specifications
{
    public class AndSpecification<T> : CompositeSpecification<T> where T : BaseEntity
    {
        public AndSpecification(Specification<T> left, Specification<T> right)
            : base(left, right)
        {
        }

        public override Expression<Func<T, bool>> ToExpression()
        {

            if(_left!=null && _right!=null)
            {
                Expression<Func<T, bool>> leftExpression = _left.ToExpression();
                Expression<Func<T, bool>> rightExpression = _right.ToExpression();

                BinaryExpression andExpression = Expression.AndAlso(
                    leftExpression.Body, rightExpression.Body.ReplaceParameter
                    (rightExpression.Parameters.Single(), leftExpression.Parameters.Single()));

                return Expression.Lambda<Func<T, bool>>(
                    andExpression, leftExpression.Parameters.Single());
            } else if(_left!=null)
            {
                return _left.ToExpression();
            } else if(_right!=null)
            {
                return _right.ToExpression();
            } else
            {
                return null;
            }
           
        }
    }
}
