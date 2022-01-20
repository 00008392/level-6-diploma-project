using BaseClasses.Specifications;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.Specifications
{
    public class SearchAccommodationSpecification : Specification<Accommodation>
    {
        private readonly string _searchText;
        public SearchAccommodationSpecification(string searchText)
        {
            _searchText = searchText;
        }
        public override Expression<Func<Accommodation, bool>> ToExpression()
        {
            return request => request.Title.Contains(_searchText) || 
            request.Description != null ? request.Description.Contains(_searchText) : false;
        }
    }
}
