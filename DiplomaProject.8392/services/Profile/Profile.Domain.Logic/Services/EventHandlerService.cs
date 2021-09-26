using AutoMapper;
using BaseClasses.Contracts;
using FluentValidation;
using Profile.Domain.Entities;
using Profile.Domain.Logic.Contracts;
using Profile.Domain.Logic.DTOs;
using Profile.Domain.Logic.Exceptions;
using Profile.Domain.Logic.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile.Domain.Logic.Services
{
    public class EventHandlerService :BaseService, IEventHandlerService
    {
        private readonly AbstractValidator<CreateProfileDTO> _validator;
        private readonly IMapper _mapper;
        public EventHandlerService(IRepositoryWithIncludes<User> repository,
            AbstractValidator<CreateProfileDTO> validator,
            IMapper mapper):base(repository)
        {
            _validator = validator;
            _mapper = mapper;
        }
        public async Task CreateUserAsync(CreateProfileDTO userDTO)
        {
            var result = _validator.Validate(userDTO);
            if(!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            await CheckUserEmailAsync(u => u.Email == userDTO.Email, userDTO.Email);
            var user = _mapper.Map<User>(userDTO);
            await _repository.CreateAsync(user);
           
        }
    }
}
