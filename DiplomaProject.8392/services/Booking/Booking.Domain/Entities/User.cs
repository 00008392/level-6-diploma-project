using BaseClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    public class User: BaseEntity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Address { get; private set; }
        public DateTime? DateOfBirth { get; private set; }
        public ICollection<BookingRequest> BookingRequestsAsMainGuest { get; }
        public ICollection<CoTravelerBooking> BookingRequestsAsCoTraveler { get; }
        public ICollection<Accommodation> Accommodations { get; }


        public User(string email)
        {
            Email = email;
        }
        public User(long id, string firstName, string lastName,
           string email, string phoneNumber,
           string address, DateTime? dateOfBirth):base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            DateOfBirth = dateOfBirth;
        }
    }
}
