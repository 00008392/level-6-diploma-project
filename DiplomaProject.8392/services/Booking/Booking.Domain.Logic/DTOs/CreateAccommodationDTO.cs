using Booking.Domain.Logic.DTOs.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.DTOs
{
    public class CreateAccommodationDTO: CreateEntityDTO
    {
       public BaseAccommodationDTO AccommodationDTO { get; private set; }
        public CreateAccommodationDTO(BaseAccommodationDTO accommodationDTO)
        {
            AccommodationDTO = accommodationDTO;
        }
    }
}
