using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.DTOs
{
    //dto for holding information about city/category/rule/facility
    //since all 4 entities have the same properties
    //it was decided to use the same dto for these entities in order to avoid duplication
    public class ItemDTO
    {
        public long Id { get; private set; }
        public string Name { get; private set; }

        public ItemDTO(
            long id,
            string name)
        {
            Id = id;
            Name = name;
        }
    }
}
