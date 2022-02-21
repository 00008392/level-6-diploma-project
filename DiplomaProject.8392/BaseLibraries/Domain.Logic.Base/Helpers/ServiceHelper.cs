using DAL.Base.Contracts;
using DAL.Base.Entities;
using FluentValidation;
using System;
using Domain.Logic.Base.Exceptions;

namespace Domain.Logic.Base.Helpers
{
    //this class contains logic common for services
    public static class ServiceHelper
    {
        //method that checks if related entity exists
        //and throws exception if not
        public static void CheckIfRelatedEntityExists<T>(
            long id,
            IRepository<T> repository)
            where T : BaseEntity
        {
            if (!repository.DoesItemWithIdExist(id))
            {
                throw new ForeignKeyViolationException($"{typeof(T).Name}");
            }
        }
        //method that handles dto validation
        public static void ValidateItem<T>(AbstractValidator<T> validator, T item)
        {
            var result = validator.Validate(item);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
