
using DAL.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Entities
{
    //base class for entities related to post (category, city, rule, facility)
    public abstract class Item: BaseEntity
    {
        public string Name { get; protected set; }
        public ICollection<Post> Posts { get; protected set; }
        protected Item(string name)
        {
            Name = name;
        }
        protected Item(
            long id,
            string name)
            : base(id)
        {
            Name = name;
        }
    }
}
