using Booking.Domain.Logic.DTOs.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.DTOs
{
    public class UpdateUserDTO: UpdateEntityDTO
    {
       public UserDTO UserDTO { get; private set; }

        public UpdateUserDTO(long id, UserDTO userDTO):base(id)
        {
            UserDTO = userDTO;
        }
    }
}
