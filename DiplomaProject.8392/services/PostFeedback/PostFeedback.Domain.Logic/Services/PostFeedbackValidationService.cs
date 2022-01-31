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
    public class PostFeedbackValidationService : IFeedbackValidationService<Entities.Post>
    {
        private readonly IRepositoryWithIncludes<User> _repository;

        public PostFeedbackValidationService(IRepositoryWithIncludes<User> repository)
        {
            _repository = repository;
        }

        public async Task<bool> CanLeaveFeedback(FeedbackDTO feedback)
        {
            //user can leave feedback on accommodation only if this user has lived in the accommodation
            //as a guest
            var user = await _repository.GetByIdAsync((long)feedback.UserId, relatedEntitiesIncluded: true);
            if(user.Bookings.Any(x=>x.PostId==feedback.ItemId && x.EndDate<DateTime.Now))
            {
                return true;
            }
            return false;
        }
    }
}
