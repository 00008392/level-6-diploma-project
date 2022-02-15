
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Entities
{
    //rule that accommodation can have
    public class Rule : Item
    {
        public Rule(
            string name) 
            : base(
                  name)
        {
        }
    }
}
