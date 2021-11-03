using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.DTOs
{
    public class CityDTO
    {
        public long Id { get; private set; }
        public string Name { get; private set; }

        public CityDTO(long id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
