
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.DTOs
{
    //dto for holding post information
    public class PostDTO
    {
        public long Id { get; private set; }
        public long OwnerId { get; private set; }
        public int MaxGuestsNo { get; private set; }

        public PostDTO(
            long id,
            long ownerId,
            int maxGuestsNo)
        {
            Id = id;
            OwnerId = ownerId;
            MaxGuestsNo = maxGuestsNo;
        }
    }
}
