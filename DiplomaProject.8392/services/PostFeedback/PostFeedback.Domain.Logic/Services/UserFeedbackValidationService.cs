using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostFeedback.Domain.Entities;
using PostFeedback.Domain.Logic.Contracts;
using PostFeedback.Domain.Logic.DTOs;
using BaseClasses.Contracts;

namespace PostFeedback.Domain.Logic.Services
{
    //this service checks whether specific user can leave feedback on other specific user
    public class UserFeedbackValidationService : IFeedbackValidationService<User>
    {
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<Booking> _bookingRepository;

        public UserFeedbackValidationService(
            IRepository<Post> postRepository,
            IRepository<Booking> bookingRepository)
        {
            _postRepository = postRepository;
            _bookingRepository = bookingRepository;
        }

        public async Task<bool> CanLeaveFeedback(FeedbackDTO feedback)
        {
           // as accommodation owner, user can leave feedback on other user if that user lived in 
            //accommodation posted by this owner
            //as guest, user can leave feedback on other user if that user is owner of accommodation
           // where this guest lived

            //to check if accommodation owner can leave feedback on guest
           // retrieve all posts by this owner and check if there are any bookings that contain
           // id of guest for whom feedback is left
            var canLeaveAsOwner = (await _postRepository.GetFilteredAsync(x => x.OwnerId == feedback.CreatorId, x => x.Bookings))
                  .Any(x => x.Bookings?.Any(y => y.GuestId == feedback.ItemId && y.EndDate.Date < DateTime.Now.Date) ?? false);
            //to check if guest can leave feedback on accommodation owner
            //retrieve all bookings by this guest and check if there are any bookings that contain accommodation
            //owned by user on whom feedback is left
            var canLeaveAsGuest = (await _bookingRepository.GetFilteredAsync(x => x.GuestId == feedback.CreatorId, x => x.Post))
                 .Any(x => x.Post.OwnerId == feedback.ItemId && x.EndDate.Date < DateTime.Now.Date);
            //if either of 2 conditions is met, user can leave feedback
            return canLeaveAsOwner || canLeaveAsGuest;

        }
    }
}
