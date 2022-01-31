using BaseClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Entities
{
    public class Category: BaseEntity
    {
        public string Name { get; private set; }
        public ICollection<Post> Posts { get; }

        public Category(string name)
        {
            Name = name;
        }
    }
}
