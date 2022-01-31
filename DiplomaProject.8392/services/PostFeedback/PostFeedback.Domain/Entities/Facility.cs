
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Entities
{
    public class Facility : Item
    {
        public Facility(string name, bool? isOther)
            : base(name, isOther)
        {
        }
    }

}
