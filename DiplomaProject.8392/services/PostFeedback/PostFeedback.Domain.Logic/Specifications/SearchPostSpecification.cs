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
        //specification that searches for posts that contain specified search text
        private readonly string _searchText;
        public SearchPostSpecification(string searchText)
        {
            _searchText = searchText;
        }
        public override Expression<Func<Post, bool>> ToExpression()
        {
            //get all posts where search text is included in either title, description, 
            //owner first name, owner last name or owner email
            return request => request.Title.Contains(_searchText)
            || (request.Description!=null&&request.Description.Contains(_searchText))
            ||(request.Owner!=null&&(request.Owner.FirstName.Contains(_searchText)
                                       || request.Owner.LastName.Contains(_searchText)
                                       || request.Owner.Email.Contains(_searchText)));
        }
    }
}
