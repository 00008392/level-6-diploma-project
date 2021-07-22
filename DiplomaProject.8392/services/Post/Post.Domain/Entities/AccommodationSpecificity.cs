using Post.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Entities
{
    public class AccommodationSpecificity: AccommodationBase
    {
        public long SpecificityId { get; set; }
        public Specificity Specificity { get; set; }
        public string OtherSpecificity { get; set; }
    }
}
