using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ExceptionHandling
{
    public static class ResponseExtensions
    {
        public static void HandleValidationException(this IResponse response, ValidationException ex)
        {
            response.IsSuccess = false;
            var errorList = ex.Errors.ToList();
            foreach (var error in errorList)
            {
                response.AddValidationError(error.PropertyName, error.ErrorMessage);
            }
        }

        public static void HandleException(this IResponse response, Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
        }
    }
}
