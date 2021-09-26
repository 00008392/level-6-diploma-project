using AutoMapper;
using BaseClasses.Contracts;
using FluentValidation;
using Post.Domain.Core;
using Post.Domain.Entities;
using Post.Domain.Logic.Contracts;
using Post.Domain.Logic.DTOs;
using Post.Domain.Logic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.Services
{
    public class EventHandlerService : IEventHandlerService
    {
        private readonly IRepository<Owner> _repository;
        private readonly AbstractValidator<CreateUserDTO> _baseValidator;
        private readonly AbstractValidator<UpdateUserDTO> _updateValidator;
        private readonly IMapper _mapper;
        public EventHandlerService(IRepository<Owner> repository,
            AbstractValidator<CreateUserDTO> baseValidator,
            AbstractValidator<UpdateUserDTO> updateValidator,
            IMapper mapper)
        {
            _repository = repository;
            _baseValidator = baseValidator;
            _updateValidator = updateValidator;
            _mapper = mapper;
        }
        public async Task CreateUserAsync(CreateUserDTO userDTO)
        {
            var result = _baseValidator.Validate(userDTO);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            var userWithEmail = (await _repository.GetFilteredAsync(u => u.Email == userDTO.Email)).FirstOrDefault();
            if (userWithEmail != null)
            {
                throw new UniqueConstraintViolationException(nameof(userDTO.Email), userDTO.Email);
            }
            var user = _mapper.Map<Owner>(userDTO);
            await _repository.CreateAsync(user);
        }

        public async Task DeleteUserAsync(long id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException(id, nameof(Owner));
            }
            await _repository.DeleteAsync(user);
        }

        public async Task UpdateUserAsync(UpdateUserDTO userDTO)
        {
            var user = await _repository.GetByIdAsync(userDTO.Id);
            if (user == null)
            {
                throw new NotFoundException(userDTO.Id, nameof(Owner));
            }
            var result = _updateValidator.Validate(userDTO);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            var userWithEmail = (await _repository.GetFilteredAsync(u => u.Email == userDTO.Email && u.Id != userDTO.Id)).FirstOrDefault();
            if (userWithEmail != null)
            {
                throw new UniqueConstraintViolationException(nameof(userDTO.Email), userDTO.Email);
            }

            var userToUpdate = _mapper.Map<Owner>(userDTO);
            await _repository.UpdateAsync(userToUpdate);
        }
    }
}
