using AutoMapper;
using BaseClasses.Contracts;
using FluentValidation;
using Post.Domain.Core;
using Post.Domain.Entities;
using Post.Domain.Logic.Contracts;
using Post.Domain.Logic.DTOs;
using Post.Domain.Logic.Exceptions;
using Post.Domain.Logic.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.Services
{
    public class PostCRUDService : IPostCRUDService
    {
        private readonly IRepositoryWithIncludes<Accommodation> _repository;
        private readonly IRepository<User> _ownerRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<City> _cityRepository;
        private readonly AbstractValidator<AccommodationManipulationDTO> _validator;
        private readonly IMapper _mapper;
        public PostCRUDService(IRepositoryWithIncludes<Accommodation> repository, 
            IRepository<User> ownerRepository,
            IRepository<Category> categoryRepository,
            IRepository<City> cityRepository,
            AbstractValidator<AccommodationManipulationDTO> validator, 
            IMapper mapper)
        {
            _repository = repository;
            _ownerRepository = ownerRepository;
            _categoryRepository = categoryRepository;
            _cityRepository = cityRepository;
            _validator = validator;
            _mapper = mapper;
        }
        public async Task CreatePostAsync(CreatePostDTO item)
        {
            var validationResult = await _validator.ValidateAsync(item);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            if (item.CategoryId != null)
            {
                var categoryExists = _categoryRepository.DoesItemWithIdExist((long)item.CategoryId);
                if (!categoryExists)
                {
                    throw new ForeignKeyViolationException("Category");
                }
            }
            if (item.CityId != null)
            {
                if (!_cityRepository.DoesItemWithIdExist((long)item.CityId))
                {
                    throw new ForeignKeyViolationException("City");
                }
            }
            var ownerExists = _ownerRepository.DoesItemWithIdExist(item.OwnerId);
            if (!ownerExists)
            {
                throw new ForeignKeyViolationException("Owner");
            }
            var accommodation = _mapper.Map<Accommodation>(item);

            await _repository.CreateAsync(accommodation);
        }

        public async Task DeletePostAsync(long id)
        {
            var accommodation = await _repository.GetByIdAsync(id, relatedEntitiesIncluded: true);

            if(accommodation == null)
            {
                throw new NotFoundException(id, "Accommodation");
            }
            if (accommodation.DatesBooked.Any())
            {
                throw new DeleteAccommodationException(id);
            }
            await _repository.DeleteAsync(accommodation);
        }

        public async Task<AccommodationInfoDTO> GetPostByIdAsync(long id)
        {
            
            var accommodation = await _repository.GetByIdAsync(id, true);
            if(accommodation!=null)
            {
                var accommodationDTO = _mapper.Map<AccommodationInfoDTO>(accommodation);
                
                return accommodationDTO;
            }
            return null;
        }

        public async Task UpdatePostAsync(UpdatePostDTO item)
        {
            var accommodation = await _repository.GetByIdAsync(item.Id);
            if(accommodation==null)
            {
                throw new NotFoundException(item.Id, "Accommodation");
            } 
            var validationResult = await _validator.ValidateAsync(item);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            if (item.CategoryId != null)
            {
                var categoryExists = _categoryRepository.DoesItemWithIdExist((long)item.CategoryId);
                if (!categoryExists)
                {
                    throw new ForeignKeyViolationException("Category");
                }
            } 
            if (item.OwnerId!=accommodation.OwnerId)
            {
                throw new ForeignKeyViolationException("Owner");
            }


                accommodation.UpdateInfo(
                item.Title, item.Description, item.OwnerId, item.CategoryId, item.CityId,
                item.Address, item.ReferencePoint, item.ContactNumber, item.RoomsNo, item.BathroomsNo,
                item.BedsNo, item.MaxGuestsNo, item.SquareMeters, item.Price, item.Latitude, item.Longitude,
                item.IsWholeApartment, item.MovingInTime == null ? null : ((DateTime)item.MovingInTime).ToString("HH:mm"),
                item.MovingOutTime == null ? null : ((DateTime)item.MovingOutTime).ToString("HH:mm"),
                item.AdditionalInfo);
            await _repository.UpdateAsync(accommodation);
        }
    }
}
