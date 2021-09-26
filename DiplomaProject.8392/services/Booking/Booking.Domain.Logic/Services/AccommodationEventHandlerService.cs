﻿using AutoMapper;
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
    public class AccommodationEventHandlerService : IEventHandlerService<Accommodation>
    {
        private readonly AbstractValidator<AccommodationDTO> _validator;
        private readonly IRepository<Accommodation> _repository;
        private readonly IRepository<User> _ownerRepository;
        private readonly IMapper _mapper;

        public AccommodationEventHandlerService(AbstractValidator<AccommodationDTO> validator,
            IRepository<Accommodation> repository, 
            IRepository<User> ownerRepository,
            IMapper mapper)
        {
            _validator = validator;
            _repository = repository;
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }

        public async Task CreateEntityAsync(ICreateEntityDTO entityDTO)
        {
            var createAccommodationDTO = (AccommodationDTO)entityDTO;
            var validationResult = await _validator.ValidateAsync(createAccommodationDTO);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
           
            var ownerExists = _ownerRepository.DoesItemWithIdExist(createAccommodationDTO.OwnerId);
            if (!ownerExists)
            {
                throw new ForeignKeyViolationException("Owner");
            }
            var accommodation = _mapper.Map<Accommodation>(createAccommodationDTO);

            await _repository.CreateAsync(accommodation);
        }

        public async Task DeleteEntityAsync(long id)
        {
            var accommodation = await _repository.GetByIdAsync(id);
            if (accommodation == null)
            {
                throw new NotFoundException(id, accommodation.GetType().Name);
            }
            await _repository.DeleteAsync(accommodation);
        }

        public async Task UpdateEntityAsync(IEntityDTO entityDTO)
        {
            var updateAccommodationDTO = (AccommodationDTO)entityDTO;
            var accommodation = await _repository.GetByIdAsync(entityDTO.Id);
            if (accommodation == null)
            {
                throw new NotFoundException(updateAccommodationDTO.Id, accommodation.GetType().Name);
            }
            var validationResult = await _validator.ValidateAsync(updateAccommodationDTO);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
           
            if (updateAccommodationDTO.OwnerId != accommodation.OwnerId)
            {
                throw new ForeignKeyViolationException("Owner");
            }


            var accommodationToUpdate = _mapper.Map<Accommodation>(updateAccommodationDTO);
            await _repository.UpdateAsync(accommodationToUpdate);
        }
    }
}
