
using DAL.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    //user domain entity
    //booking microservice needs only id of user to handle booking logic
    //id value is received from account microservice
    public class User: BaseEntity
    {
        public User(long id) 
            : base(id)
        {
        }
        public ICollection<Booking> Bookings { get; private set; }
        public ICollection<Post> Posts { get; private set; }
        
    }
}
