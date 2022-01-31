using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.DTOs
{
    public class CategoryCityDTO
    {
        public long Id { get; private set; }
        public string Name { get; private set; }

        public CategoryCityDTO(long id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
