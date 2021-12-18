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
        private readonly IRepository<Owner> _userRepository;
        private readonly IRepository<Accommodation> _accommodationRepository;
        private readonly IRepository<DatesBooked> _bookingRepository;
        private readonly AbstractValidator<CreateUserDTO> _baseUserValidator;
        private readonly AbstractValidator<UpdateUserDTO> _updateUserValidator;
        private readonly AbstractValidator<AddBookingDTO> _bookingValidator;
        private readonly IMapper _mapper;

        public EventHandlerService(IRepository<Owner> userRepository,
                                   IRepository<Accommodation> accommodationRepository,
                                   IRepository<DatesBooked> bookingRepository,
                                   AbstractValidator<CreateUserDTO> baseUserValidator,
                                   AbstractValidator<UpdateUserDTO> updateUserValidator,
                                   AbstractValidator<AddBookingDTO> bookingValidator,
                                   IMapper mapper)
        {
            _userRepository = userRepository;
            _accommodationRepository = accommodationRepository;
            _bookingRepository = bookingRepository;
            _baseUserValidator = baseUserValidator;
            _updateUserValidator = updateUserValidator;
            _bookingValidator = bookingValidator;
            _mapper = mapper;
        }

        public async Task AddBookingAsync(AddBookingDTO bookingDTO)
        {
            if(!_accommodationRepository.DoesItemWithIdExist(bookingDTO.AccommodationId))
            {
                throw new ForeignKeyViolationException("Accommodation");
            }
            var result = _bookingValidator.Validate(bookingDTO);
            if(!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            var booking = _mapper.Map<DatesBooked>(bookingDTO);
            await _bookingRepository.CreateAsync(booking);
        }

        public async Task CreateUserAsync(CreateUserDTO userDTO)
        {
            var result = _baseUserValidator.Validate(userDTO);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            var userWithEmail = (await _userRepository.GetFilteredAsync(u => u.Email == userDTO.Email)).FirstOrDefault();
            if (userWithEmail != null)
            {
                throw new UniqueConstraintViolationException(nameof(userDTO.Email), userDTO.Email);
            }
            var user = _mapper.Map<Owner>(userDTO);
            await _userRepository.CreateAsync(user);
        }

        public async Task DeleteUserAsync(long id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException(id, nameof(Owner));
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

        public async Task UpdateUserAsync(UpdateUserDTO userDTO)
        {
            var user = await _userRepository.GetByIdAsync(userDTO.Id);
            if (user == null)
            {
                throw new NotFoundException(userDTO.Id, nameof(Owner));
            }
            var result = _updateUserValidator.Validate(userDTO);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            var userWithEmail = (await _userRepository.GetFilteredAsync(u => u.Email == userDTO.Email && u.Id != userDTO.Id)).FirstOrDefault();
            if (userWithEmail != null)
            {
                throw new UniqueConstraintViolationException(nameof(userDTO.Email), userDTO.Email);
            }

            var userToUpdate = _mapper.Map<Owner>(userDTO);
            await _userRepository.UpdateAsync(userToUpdate);
        }
    }
}
