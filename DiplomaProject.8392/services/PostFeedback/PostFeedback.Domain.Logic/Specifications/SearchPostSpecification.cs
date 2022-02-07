using BaseClasses.Specifications;
using PostFeedback.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Specifications
{
    public class SearchPostSpecification : Specification<Post>
    {
        private readonly string _searchText;
        public SearchPostSpecification(string searchText)
        {
            _searchText = searchText;
        }
        public override Expression<Func<Post, bool>> ToExpression()
        {
            //!!!
            return request => (request.Title.Contains(_searchText) || (
            request.Description != null) && request.Description.Contains(_searchText));
        }
    }
}
