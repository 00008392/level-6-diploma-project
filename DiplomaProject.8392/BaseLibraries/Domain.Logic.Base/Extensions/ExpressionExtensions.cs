using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Logic.Base.Extensions
{
   public static class ExpressionExtensions
    {
        public static Expression ReplaceParameter(this Expression target, ParameterExpression parameter, Expression value)
        {
            return new ParameterReplacer { Parameter = parameter, Value = value }.Visit(target);
        }

        class ParameterReplacer : ExpressionVisitor
        {
            public ParameterExpression Parameter;
            public Expression Value;
            protected override Expression VisitParameter(ParameterExpression node)
            {
                return node == Parameter ? Value : node;
            }
        }
    }
}
