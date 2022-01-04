using BaseClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Core
{
    public abstract class FeedbackEntity : BaseEntity
    {
        protected FeedbackEntity()
        {
        }

        protected FeedbackEntity(long id) : base(id)
        {
        }
    }
}
