using Grpc.Base.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Protos.Common
{
    //implements this interface for proper display of errors
    public partial class Response : IResponse
    {
        //method for proper display of validation error
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
