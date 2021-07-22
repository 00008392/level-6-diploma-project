using Post.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Entities
{
    public class Specificity: BaseEntity
    {
        public string Name { get; set; }
        public ICollection<AccommodationSpecificity> AccommodationSpecificities { get; set; }
    }
}
