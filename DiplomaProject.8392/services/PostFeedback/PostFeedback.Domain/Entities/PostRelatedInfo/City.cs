
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Entities
{
    //city where accommodation is located
   public class City: Item
    {
        public City(
            long id,
            string name)
            :base(id, name)
        {
        }
        public City(string name)
            :base(name)
        {
        }
    }
}
