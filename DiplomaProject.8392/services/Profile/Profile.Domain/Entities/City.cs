﻿
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
        public string Name { get; set; }
        public long CountryId { get; set; }
        public Country Country { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
