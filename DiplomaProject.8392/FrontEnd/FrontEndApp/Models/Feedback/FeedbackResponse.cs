using FrontEndApp.Models.Feedback.Core;
using FrontEndApp.Models.Post;
using FrontEndApp.Models.User;
using FrontEndApp.Models.User.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models.Feedback
{
    public class FeedbackResponse: FeedbackBase
    {
        public long Id { get; set; }
		public UserResponse FeedbackCreator { get; set; }
		public UserResponse User { get; set; }
		public PostResponse Accommodation { get; set; }
		public DateTime DatePublished { get; set; }
		public bool NoItem { get; set; } = false;
	}
}
