﻿using ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.API
{
    public partial class Response : IResponse
    {
        public void AddValidationError(string propertyName, string errorMessage)
        {
            Errors.Add(new Error
            {
                PropertyName = propertyName,
                Message = errorMessage
            });
        }
    }
}