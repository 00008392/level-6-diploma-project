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
        private readonly AbstractValidator<BaseAccommodationDTO> _validator;
        private readonly IRepository<Accommodation> _repository;
        private readonly IRepository<User> _ownerRepository;

        public AccommodationEventHandlerService(AbstractValidator<BaseAccommodationDTO> validator,
            IRepository<Accommodation> repository, 
            IRepository<User> ownerRepository)
        {
            _validator = validator;
            _repository = repository;
            _ownerRepository = ownerRepository;
        }

        public async Task CreateEntityAsync(CreateEntityDTO entityDTO)
        {
            var createAccommodationDTO = (CreateAccommodationDTO)entityDTO;
            var baseAccommodationDTO = createAccommodationDTO.AccommodationDTO;
            var validationResult = await _validator.ValidateAsync(baseAccommodationDTO);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
           
            var ownerExists = _ownerRepository.DoesItemWithIdExist(baseAccommodationDTO.OwnerId);
            if (!ownerExists)
            {
                throw new ForeignKeyViolationException("Owner");
            }
            var accommodation = new Accommodation(baseAccommodationDTO.Title, baseAccommodationDTO.OwnerId,
                baseAccommodationDTO.Address, baseAccommodationDTO.ContactNumber, baseAccommodationDTO.RoomsNo,
                baseAccommodationDTO.BathroomsNo, baseAccommodationDTO.BedsNo, baseAccommodationDTO.MaxGuestsNo,
                baseAccommodationDTO.SquareMeters, baseAccommodationDTO.Price,
                baseAccommodationDTO.IsWholeApartment, baseAccommodationDTO.MovingInTime,
                baseAccommodationDTO.MovingOutTime
                );

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

        public async Task UpdateEntityAsync(UpdateEntityDTO entityDTO)
        {
            var updateAccommodationDTO = (UpdateAccommodationDTO)entityDTO;
            var accommodation = await _repository.GetByIdAsync(entityDTO.Id);
            if (accommodation == null)
            {
                throw new NotFoundException(updateAccommodationDTO.Id, accommodation.GetType().Name);
            }
            var baseAccommodationDTO = updateAccommodationDTO.AccommodationDTO;
            var validationResult = await _validator.ValidateAsync(baseAccommodationDTO);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
           
            if (baseAccommodationDTO.OwnerId != accommodation.OwnerId)
            {
                throw new ForeignKeyViolationException("Owner");
            }


            var accommodationToUpdate = new Accommodation(baseAccommodationDTO.Title,
                baseAccommodationDTO.OwnerId, baseAccommodationDTO.Address,
                baseAccommodationDTO.ContactNumber, baseAccommodationDTO.RoomsNo,
                baseAccommodationDTO.BathroomsNo, baseAccommodationDTO.BedsNo,
                baseAccommodationDTO.MaxGuestsNo, baseAccommodationDTO.SquareMeters,
                baseAccommodationDTO.Price, baseAccommodationDTO.IsWholeApartment,
                baseAccommodationDTO.MovingInTime, baseAccommodationDTO.MovingOutTime);
            await _repository.UpdateAsync(accommodationToUpdate);
        }
    }
}
