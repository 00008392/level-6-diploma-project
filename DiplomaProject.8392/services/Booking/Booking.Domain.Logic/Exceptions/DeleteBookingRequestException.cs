﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.Exceptions
{
    public class DeleteBookingRequestException: Exception
    {
        public DeleteBookingRequestException()
            :base("Booking request cannot be deleted")
        {

        }
    }
}
