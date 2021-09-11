using BaseClasses.Contracts;
using Booking.Domain.Entities;
using Booking.Domain.Logic.Contracts;
using Booking.Domain.Logic.DTOs;
using Booking.Domain.Logic.DTOs.Core;
using Booking.Domain.Logic.Exceptions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.Services
{
    public class UserEventHandlerService : IEventHandlerService<User>
    {
        private readonly AbstractValidator<CreateUserDTO> _createUserValidator;
        private readonly AbstractValidator<UserDTO> _updateUserValidator;
        private readonly IRepository<User> _repository;

        public UserEventHandlerService(AbstractValidator<CreateUserDTO> createUserValidator,
            AbstractValidator<UserDTO> updateUserValidator,
            IRepository<User> repository)
        {
            _createUserValidator = createUserValidator;
            _updateUserValidator = updateUserValidator;
            _repository = repository;
        }

        public async Task CreateEntity(CreateEntityDTO entityDTO)
        {
            var userDTO = (CreateUserDTO)entityDTO;
            var result = _createUserValidator.Validate(userDTO);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            var userWithEmail = (await _repository.GetFilteredAsync(u => u.Email == userDTO.Email)).FirstOrDefault();
            if (userWithEmail != null)
            {
                throw new UniqueConstraintViolationException(nameof(userDTO.Email), userDTO.Email);
            }
            var user = new User(userDTO.Email);
            await _repository.CreateAsync(user);
        }

        public async Task DeleteEntity(long id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException(id, user.GetType().Name);
            }
            await _repository.DeleteAsync(user);
        }

        public async Task UpdateEntity(UpdateEntityDTO entityDTO)
        {
            var user = await _repository.GetByIdAsync(entityDTO.Id);
            if (user == null)
            {
                throw new NotFoundException(entityDTO.Id, user.GetType().Name);
            }
            var updateUserDTO = (UpdateUserDTO)entityDTO;
            var userBaseDTO = updateUserDTO.UserDTO;
            var result = _updateUserValidator.Validate(userBaseDTO);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            var userWithEmail = (await _repository.GetFilteredAsync(u => u.Email == userBaseDTO.Email && u.Id != updateUserDTO.Id)).FirstOrDefault();
            if (userWithEmail != null)
            {
                throw new UniqueConstraintViolationException(nameof(userBaseDTO.Email), userBaseDTO.Email);
            }

            var userToUpdate = new User(user.Id, userBaseDTO.FirstName, userBaseDTO.LastName,
                userBaseDTO.Email, userBaseDTO.PhoneNumber, userBaseDTO.Address, userBaseDTO.DateOfBirth);
            await _repository.UpdateAsync(userToUpdate);
        }
    }
}
