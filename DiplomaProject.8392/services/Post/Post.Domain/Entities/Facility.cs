﻿using Post.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Entities
{
    public class Facility: ItemBase
    {
        public ICollection<AccommodationFacility> AccommodationFacilities { get; set; }
    }
}
