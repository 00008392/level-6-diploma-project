using BaseClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Entities
{
    public class Category: BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Accommodation> Accommodations { get; set; }
    }
}
