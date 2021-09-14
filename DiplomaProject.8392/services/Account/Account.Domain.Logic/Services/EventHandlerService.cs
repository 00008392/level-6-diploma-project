
using Account.Domain.Entities;
using Account.Domain.Logic.Contracts;
using Account.Domain.Logic.Core;
using Account.Domain.Logic.DTOs;
using Account.Domain.Logic.Exceptions;
using BaseClasses.Contracts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.Services
{
    public class EventHandlerService :BaseService, IEventHandlerService
    {
        private readonly AbstractValidator<UpdateUserDTO> _validator;
        public EventHandlerService(IRepository<User> repository,
            AbstractValidator<UpdateUserDTO> validator):base(repository)
        {
            _validator = validator;
        }
        public async Task DeleteUserAsync(long id)
        {
            var user = await FindUserAsync(id);
            await _repository.DeleteAsync(user);
        }

        public async Task UpdateUserAsync(UpdateUserDTO userDTO)
        {
            var user =await FindUserAsync(userDTO.Id);
            var result = _validator.Validate(userDTO);
            if(!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
           await CheckUserEmail(u => u.Email == userDTO.Email && u.Id != userDTO.Id, userDTO.Email);
            user.ChangeEmail(userDTO.Email);
            await _repository.UpdateAsync(user);
        }

      
    }
}
