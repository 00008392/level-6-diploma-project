using FluentValidation;
using Grpc.Base.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grpc.Base.Extensions
{
    //extension method for Response grpc generated classes for proper exception handling and display of errors
    public static class ResponseExtensions
    {
        //handle exception related to validation
        public static void HandleValidationException(this IResponse response, ValidationException ex)
        {
            //display validation errors
            response.IsSuccess = false;
            var errorList = ex.Errors.ToList();
            foreach (var error in errorList)
            {
                response.AddValidationError(error.PropertyName, error.ErrorMessage);
            }
        }
        //handle other exceptions
        public static void HandleException(this IResponse response, Exception ex)
        {
            //display error message
            response.IsSuccess = false;
            response.Message = ex.Message;
        }
    }
}
