using BaseClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Entities
{
   public class Booking: BaseEntity
    {
        public long OwnerId { get; private set; }
        public User Owner { get; private set; }
        public long GuestId { get; private set; }
        public User Guest { get; private set; }

        public Booking(long id, long ownerId, User owner, long guestId, User guest)
            :base(id)
        {
            OwnerId = ownerId;
            Owner = owner;
            GuestId = guestId;
            Guest = guest;
        }
        public Booking(long id, long ownerId, long guestId)
            :base(id)
        {
            Id = id;
            OwnerId = ownerId;
            GuestId = guestId;
        }
    }
}
