using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Filter
{
    //parameters to return paginated results
    public class PaginationParameters
    {
        //how many items to skip
        public int? Skip { get; set; }
        //how many items to return
        public int? Take { get; set; }
    }
}
