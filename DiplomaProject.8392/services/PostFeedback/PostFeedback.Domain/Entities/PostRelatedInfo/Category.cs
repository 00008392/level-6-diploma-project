using BaseClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Entities
{
    //category of accommodation indicated in post
    public class Category: PostRelatedInfoItem
    {
        public Category(
           long id,
           string name)
           : base(id, name)
        {
        }
        public Category(string name)
            : base(name)
        {
        }
    }
}
