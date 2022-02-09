using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.DTOs
{
    //dto for holding information either about a category or a city
    //it was decided to use the same dto for both entities in order to avoid duplication
    //because they have the same properties
    public class CategoryCityDTO
    {
        public long Id { get; private set; }
        public string Name { get; private set; }

        public CategoryCityDTO(
            long id,
            string name)
        {
            Id = id;
            Name = name;
        }
    }
}
