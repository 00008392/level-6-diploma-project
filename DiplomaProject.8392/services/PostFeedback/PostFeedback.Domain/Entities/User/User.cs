using BaseClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Entities
{
    //user domain entity
    public class User: FeedbackEntity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        //for posts the user is owner
        public ICollection<Post> Posts { get; private set; }
        //for bookings the user is guest who books accommodation
        public ICollection<Booking> Bookings { get; private set; }
        //feedbacks left for this user
        public ICollection<Feedback<User>> Feedbacks { get; private set; }
        //feedbacks left by this user
        public ICollection<Feedback<User>> FeedbacksForUsers { get; private set; }
        public ICollection<Feedback<Post>> FeedbacksForAccommodations { get; private set; }
        public User(
            long id,
            string firstName,
            string lastName,
            string email) :base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

    }
}
