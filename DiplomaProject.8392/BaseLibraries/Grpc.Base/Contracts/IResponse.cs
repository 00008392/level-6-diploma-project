using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grpc.Base.Contracts
{
    //this interface is implemented by Response grpc generated classes in each microservice for proper exception handling and display of errors
    public interface IResponse
    {
        bool IsSuccess { set; }
        string Message { set; }
        public void AddValidationError(string propertyName, string message);
    }
}
