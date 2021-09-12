﻿using Booking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.Exceptions
{
   public class BookingRequestStatusException: Exception
    {
        public BookingRequestStatusException(Status status):
            base($"Status of request cannot be changed to {status}")
        {

        }
    }
}
