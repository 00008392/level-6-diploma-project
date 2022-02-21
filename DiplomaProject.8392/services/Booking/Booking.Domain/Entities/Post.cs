
using DAL.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    //post domain entity
    //post about accommodation for rent
    //booking microservice needs only id of post, id of owner and maximum number of guests
    //that accommodation can have
    //these values are received from post microservice
   public class Post: BaseEntity
    {
        public long OwnerId { get; private set; }
        public User Owner { get; private set; }
        public int MaxGuestsNo { get; private set; }
        public ICollection<Booking> Bookings { get; private set; }
        public Post(
            long id,
            long ownerId,
            int maxGuestsNo)
            :base(id)
        {
            OwnerId = ownerId;
            MaxGuestsNo = maxGuestsNo;
        }
    }
}
