using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Base.Contracts;
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

        public async Task<bool> CanLeaveFeedback(long creatorId, long postId)
        {
            if (creatorId == 0 || postId == 0)
            {
                return false;
            }
            // user can leave feedback on accommodation specified in post
            // only if this user lived in the accommodation as a guest
            // in list of bookings, find booking on specified accommodation where end date already passed
            var user = await _repository.GetByIdAsync(creatorId, x => x.Bookings);
            return user.Bookings.Any(x => x.PostId == postId && x.EndDate.Date < DateTime.Now.Date);

        }
    }
}
