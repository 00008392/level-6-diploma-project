
using DAL.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Entities
{
    //need this base class for entities on which feedback can be left (user and accommodation)
    public abstract class FeedbackEntity : BaseEntity
    {
        protected FeedbackEntity()
        {
        }

        protected FeedbackEntity(long id)
            : base(id)
        {
        }
    }
}
