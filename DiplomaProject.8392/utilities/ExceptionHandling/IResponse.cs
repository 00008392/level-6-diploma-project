using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandling
{
    public interface IResponse
    {
        bool IsSuccess { set; }
        string Message { set; }
        public void AddValidationError(string propertyName, string message);
    }
}
