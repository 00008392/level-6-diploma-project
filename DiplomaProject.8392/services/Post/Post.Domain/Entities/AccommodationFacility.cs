using Post.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Entities
{
    public class AccommodationFacility: AccommodationBase
    {
        public long FacilityId { get; set; }
        public Facility Facility { get; set; }
        public string OtherFacility { get; set; }
    }
}
