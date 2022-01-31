
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Entities
{
    public class Specificity : Item
    {
        public Specificity(string name, bool? isOther)
            : base(name, isOther)
        {
        }
    }
}
