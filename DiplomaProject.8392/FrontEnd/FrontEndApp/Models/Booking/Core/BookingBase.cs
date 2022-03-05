using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models.Booking.Core
{
    public class BookingBase
    {
        [Required(ErrorMessage ="Number of guests is required")]
        [Range(1, 20, ErrorMessage = "Guest limit is 20")]
        public int? GuestNo { get; set; }
        [Required(ErrorMessage ="Start date is required")]
        public DateTime? StartDate { get; set; }
        [Required(ErrorMessage = "End date is required")]
        public DateTime? EndDate { get; set; }
    }
}
