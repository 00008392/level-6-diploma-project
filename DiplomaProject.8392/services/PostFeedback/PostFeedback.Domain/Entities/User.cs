using BaseClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Entities
{
    public class User: FeedbackEntity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public ICollection<Post> Posts { get; }
        public ICollection<Booking> Bookings { get; }
        public ICollection<Feedback<User>> Feedbacks { get; }
        public ICollection<Feedback<User>> FeedbacksForUsers { get; }
        public ICollection<Feedback<Post>> FeedbacksForAccommodations { get; }

        public User(long id, string firstName, string lastName,
            string email, string phoneNumber):base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public User(string email, string firstName, string lastName)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }

    }
}
