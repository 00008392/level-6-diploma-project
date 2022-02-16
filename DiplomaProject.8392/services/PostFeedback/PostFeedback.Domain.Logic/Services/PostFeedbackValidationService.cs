using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseClasses.Contracts;
using PostFeedback.Domain.Entities;
using PostFeedback.Domain.Logic.Contracts;
using PostFeedback.Domain.Logic.DTOs;

namespace PostFeedback.Domain.Logic.Services
{
    //this service checks whether specific user can leave feedback on specific accommodation
    public class PostFeedbackValidationService : IFeedbackValidationService<Post>
    {
        private readonly IRepository<User> _repository;

        public PostFeedbackValidationService(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<bool> CanLeaveFeedback(FeedbackDTO feedback)
        {
           // user can leave feedback on accommodation specified in post
           // only if this user lived in the accommodation as a guest
           // in list of bookings, find booking on specified accommodation where end date already passed
             var user = await _repository.GetByIdAsync((long)feedback.CreatorId, x => x.Bookings);
            return user.Bookings.Any(x => x.PostId == feedback.ItemId && x.EndDate.Date < DateTime.Now.Date);

        }
    }
}
