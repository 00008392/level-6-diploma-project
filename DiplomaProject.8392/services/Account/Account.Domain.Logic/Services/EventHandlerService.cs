
using Account.Domain.Entities;
using Account.Domain.Logic.Contracts;
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
    public class EventHandlerService : IEventHandlerService
    {
        private readonly IRepository<User> _repository;
        private readonly AbstractValidator<UpdateUserDTO> _validator;
        public EventHandlerService(IRepository<User> repository,
            AbstractValidator<UpdateUserDTO> validator)
        {
            _repository = repository;
            _validator = validator;
        }
        public async Task DeleteUserAsync(long id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
            {
                throw new AccountNotFoundException(id);
            }
            await _repository.DeleteAsync(user);
        }

        public async Task UpdateUserAsync(UpdateUserDTO userDTO)
        {
            var user = await _repository.GetByIdAsync(userDTO.Id);
            if (user == null)
            {
                throw new AccountNotFoundException(userDTO.Id);
            }
            var result = _validator.Validate(userDTO);
            if(!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            var userWithEmail = (await _repository.GetFilteredAsync(u => u.Email == userDTO.Email && u.Id != userDTO.Id)).FirstOrDefault();
            if (userWithEmail != null)
            {
                throw new UniqueConstraintViolationException(nameof(userDTO.Email), userDTO.Email);
            }
            user.Email = userDTO.Email;
            await _repository.UpdateAsync(user);
        }
    }
}
