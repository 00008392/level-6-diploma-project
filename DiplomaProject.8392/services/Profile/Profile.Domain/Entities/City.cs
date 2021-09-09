
using BaseClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile.Domain.Entities
{
    public class City: BaseEntity
    {
        public string Name { get; private set; }
        public long CountryId { get; private set; }
        public Country Country { get; }
        public ICollection<User> Users { get; }

        public City(string name, long countryId)
        {
            Name = name;
            CountryId = countryId;
        }
    }
}
