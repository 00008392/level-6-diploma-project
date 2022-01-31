using AutoMapper;
using BaseClasses.Contracts;
using FluentValidation;
using PostFeedback.Domain.Entities;
using PostFeedback.Domain.Logic.Contracts;
using PostFeedback.Domain.Logic.DTOs;
using PostFeedback.Domain.Logic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Services
{
    public class EventHandlerService : IEventHandlerService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<Booking> _bookingRepository;
        private readonly AbstractValidator<UserDTO> _userValidator;
        private readonly AbstractValidator<AddBookingDTO> _bookingValidator;
        private readonly IMapper _mapper;

        public EventHandlerService(IRepository<User> userRepository,
                                   IRepository<Post> postRepository,
                                   IRepository<Booking> bookingRepository,
                                   AbstractValidator<UserDTO> userValidator,
                                   AbstractValidator<AddBookingDTO> bookingValidator,
                                   IMapper mapper)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
            _bookingRepository = bookingRepository;
            _userValidator = userValidator;
            _bookingValidator = bookingValidator;
            _mapper = mapper;
        }

        public async Task AddBookingAsync(AddBookingDTO bookingDTO)
        {
            if(!_postRepository.DoesItemWithIdExist(bookingDTO.PostId))
            {
                throw new ForeignKeyViolationException(nameof(Post));
            }
            var result = _bookingValidator.Validate(bookingDTO);
            if(!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            var booking = _mapper.Map<Booking>(bookingDTO);
            await _bookingRepository.CreateAsync(booking);
        }

        public async Task CreateUserAsync(UserDTO userDTO)
        {
            var result = _userValidator.Validate(userDTO);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            var userWithEmail = (await _userRepository.GetFilteredAsync(u => u.Email == userDTO.Email)).FirstOrDefault();
            if (userWithEmail != null)
            {
                throw new UniqueConstraintViolationException(nameof(userDTO.Email), userDTO.Email);
            }
            var user = _mapper.Map<User>(userDTO);
            await _userRepository.CreateAsync(user);
        }

        public async Task DeleteUserAsync(long id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException(id, nameof(User));
            }
            await _userRepository.DeleteAsync(user);
        }

        public async Task RemoveBookingAsync(long id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if(booking==null)
            {
                throw new NotFoundException(id, "Booking");
            }
            await _bookingRepository.DeleteAsync(booking);
        }

        public async Task UpdateUserAsync(UserDTO userDTO)
        {
            var user = await _userRepository.GetByIdAsync(userDTO.Id);
            if (user == null)
            {
                throw new NotFoundException(userDTO.Id, nameof(User));
            }
            var result = _userValidator.Validate(userDTO);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            var userWithEmail = (await _userRepository.GetFilteredAsync(u => u.Email == userDTO.Email && u.Id != userDTO.Id)).FirstOrDefault();
            if (userWithEmail != null)
            {
                throw new UniqueConstraintViolationException(nameof(userDTO.Email), userDTO.Email);
            }

            var userToUpdate = _mapper.Map<User>(userDTO);
            await _userRepository.UpdateAsync(userToUpdate);
        }
    }
}
