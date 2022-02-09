using BaseClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Entities
{
    //represents country domain entity
    //This entity represents country where user lives
    public class Country : BaseEntity
    {
        public string Name { get; private set; }
        public ICollection<User> Users { get; private set; }

        public Country(string name)
        {
            Name = name;
        }
        public Country(
            long id,
            string name)
            :base(id)
        {
            Name = name;
        }
    }
}
