using FrontEndApp.Services.Booking.Contracts;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Components
{
    //component that all booking components inherit from
    public class BookingBaseComponent: CustomBaseComponent
    {
        [Inject]
        protected IBookingService _service { get; set; }
    }
}
