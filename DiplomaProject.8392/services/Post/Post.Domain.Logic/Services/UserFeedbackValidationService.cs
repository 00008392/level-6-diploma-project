using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Post.Domain.Entities;
using Post.Domain.Logic.Contracts;
using Post.Domain.Logic.DTOs;
using BaseClasses.Contracts;

namespace Post.Domain.Logic.Services
{
    public class UserFeedbackValidationService : IFeedbackValidationService<User>
    {
        private readonly IRepositoryWithIncludes<User> _userRepository;

        public UserFeedbackValidationService(
            IRepositoryWithIncludes<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> CanLeaveFeedback(FeedbackDTO feedback)
        {
            //user can leave feedback on other user only as on accommodation owner or as on guest

            var user = await _userRepository.GetByIdAsync((long)feedback.UserId,
                relatedEntitiesIncluded: true);
            if (user.Bookings.Any(x => x.Accommodation.OwnerId == feedback.ItemId && x.EndDate < DateTime.Now)
             || user.Accommodations.Any(x=>x.Bookings.Any(y=>y.UserId==feedback.ItemId && y.EndDate < DateTime.Now)))
            {

                return true;
            }
            return false;
        }
    }
}
