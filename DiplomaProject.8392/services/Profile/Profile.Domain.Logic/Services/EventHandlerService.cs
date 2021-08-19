using FluentValidation;
using Profile.Domain.Core;
using Profile.Domain.Entities;
using Profile.Domain.Logic.Contracts;
using Profile.Domain.Logic.DTOs;
using Profile.Domain.Logic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile.Domain.Logic.Services
{
    public class EventHandlerService : IEventHandlerService
    {
        private readonly IRepository<User> _repository;
        private readonly AbstractValidator<CreateProfileDTO> _validator;
        public EventHandlerService(IRepository<User> repository,
            AbstractValidator<CreateProfileDTO> validator)
        {
            _repository = repository;
            _validator = validator;
        }
        public async Task CreateUserAsync(CreateProfileDTO userDTO)
        {
            var result = _validator.Validate(userDTO);
            if(!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            var userWithEmail = (await _repository.GetFilteredAsync(u => u.Email == userDTO.Email)).FirstOrDefault();
            if (userWithEmail != null)
            {
                throw new UniqueConstraintViolationException(nameof(userDTO.Email), userDTO.Email);
            }
            var user = new User
            {
                Email = userDTO.Email,
                RegistrationDate = userDTO.RegistrationDate
            };
            await _repository.CreateAsync(user);
           
        }
    }
}
