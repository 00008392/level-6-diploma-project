using BaseClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Entities
{
    public class Country : BaseEntity
    {
        public string Name { get; private set; }
        public ICollection<City> Cities { get; private set; }

        public Country(string name)
        {
            Name = name;
        }
    }
}
