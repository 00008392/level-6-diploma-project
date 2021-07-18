using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account.API.ExceptionHandling
{
    public static class ExceptionHandler
    {
        public static Response HandleValidationException(ValidationException ex)
        {
            var response = new Response
            {
                IsSuccess = false
            };
            var errorList = ex.Errors.ToList();
            foreach (var error in errorList)
            {
                response.Errors.Add(new Error
                {
                    PropertyName = error.PropertyName,
                    Message = error.ErrorMessage
                });
            }
            return response;
        }
        public static Response HandleException(Exception ex)
        {
            var response = new Response
            {
                IsSuccess = false,
                Message = ex.Message
            };
            return response;
        }
    }
}
