using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

    namespace PostFeedback.API
    {
        //correctly display date time properties in json format
        public partial class FilterRequest
        {
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
        }
    }

