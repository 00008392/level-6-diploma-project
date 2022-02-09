using PostFeedback.Domain.Entities;
using PostFeedback.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Contracts
{
    //there are certain constraints on leaving feedbacks
    //for example, user can leave feedbacks only on the accommodations in which this user
    //lived as guest
    //this service checks whether user can leave feedback on specific user/accommodation
    public interface IFeedbackValidationService<T> where T: FeedbackEntity
    {
        Task<bool> CanLeaveFeedback(FeedbackDTO feedback);
    }
}
