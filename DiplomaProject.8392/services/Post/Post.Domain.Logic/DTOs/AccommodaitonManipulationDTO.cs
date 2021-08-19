using Post.Domain.Logic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.DTOs
{
   public abstract class AccommodaitonManipulationDTO: BaseAccommodationDTO
    {
        public DateTime? MovingInTime { get; set; }
        public DateTime? MovingOutTime { get; set; }
    }
}
