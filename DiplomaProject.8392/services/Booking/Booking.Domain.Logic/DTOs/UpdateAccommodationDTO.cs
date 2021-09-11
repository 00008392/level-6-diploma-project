using Booking.Domain.Logic.DTOs.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.DTOs
{
    public class UpdateAccommodationDTO : UpdateEntityDTO
    {
        public BaseAccommodationDTO AccommodationDTO { get; private set; }

        public UpdateAccommodationDTO(long id,BaseAccommodationDTO accommodationDTO)
            :base(id)
        {
            AccommodationDTO = accommodationDTO;
        }
    }
}
