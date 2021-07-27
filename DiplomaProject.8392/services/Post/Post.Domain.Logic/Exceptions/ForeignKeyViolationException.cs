﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.Exceptions
{
    public class ForeignKeyViolationException: Exception
    {
        public ForeignKeyViolationException(string property) : base($"Invalid {property}")
        {

        }
    }
}
