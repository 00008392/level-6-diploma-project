using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseClasses.Contracts;
using Post.Domain.Entities;
using Post.Domain.Logic.Contracts;
using Post.Domain.Logic.DTOs;

namespace Post.Domain.Logic.Services
{
    public class AccommodationFeedbackValidationService : IFeedbackValidationService<Accommodation>
    {
        private readonly IRepositoryWithIncludes<User> _repository;

        public AccommodationFeedbackValidationService(IRepositoryWithIncludes<User> repository)
        {
            _repository = repository;
        }

        public async Task<bool> CanLeaveFeedback(FeedbackDTO feedback)
        {
            //user can leave feedback on accommodation only if this user has lived in the accommodation
            //as a guest
            var user = await _repository.GetByIdAsync((long)feedback.UserId, relatedEntitiesIncluded: true);
            if(user.Bookings.Any(x=>x.AccommodationId==feedback.ItemId && x.EndDate<DateTime.Now))
            {
                return true;
            }
            return false;
        }
    }
}
