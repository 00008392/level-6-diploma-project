
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Entities
{
    //facility that accommodation can have
    public class Facility : Item
    {
        public Facility(
            string name)
            : base(
                  name)
        {
        }
    }

}
