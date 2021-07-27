﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.Exceptions
{
    public class NotFoundException: Exception
    {
        public NotFoundException(long id, string property) : base($"{property} with id = {id} does not exist")
        {

        }
    }
}
