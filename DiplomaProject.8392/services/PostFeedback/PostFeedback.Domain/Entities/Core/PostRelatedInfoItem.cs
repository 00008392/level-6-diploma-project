using BaseClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Entities
{
    //base class for entities related to post (category and city entities)
    public abstract class PostRelatedInfoItem: BaseEntity
    {
        public string Name { get; protected set; }
        public ICollection<Post> Posts { get; protected set; }
        protected PostRelatedInfoItem(string name)
        {
            Name = name;
        }
        protected PostRelatedInfoItem(
            long id,
            string name) : base(id)
        {
            Name = name;
        }
    }
}
