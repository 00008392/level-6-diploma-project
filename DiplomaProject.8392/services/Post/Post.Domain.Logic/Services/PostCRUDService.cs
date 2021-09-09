using BaseClasses.Contracts;
using FluentValidation;
using Post.Domain.Core;
using Post.Domain.Entities;
using Post.Domain.Logic.Contracts;
using Post.Domain.Logic.Core;
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
        private readonly IPostRepository _repository;
        private readonly IRepository<Owner> _ownerRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly AbstractValidator<AccommodaitonManipulationDTO> _validator;
        public PostCRUDService(IPostRepository repository, 
            IRepository<Owner> ownerRepository,
            IRepository<Category> categoryRepository,
            AbstractValidator<AccommodaitonManipulationDTO> validator)
        {
            _repository = repository;
            _ownerRepository = ownerRepository;
            _categoryRepository = categoryRepository;
            _validator = validator;
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
            var ownerExists = _ownerRepository.DoesItemWithIdExist(item.OwnerId);
            if (!ownerExists)
            {
                throw new ForeignKeyViolationException("Owner");
            }
            var accommodation = new Accommodation(item.Title, item.Description, item.OwnerId,
                DateTime.Now, item.CategoryId, item.Address, item.ReferencePoint, item.ContactNumber,
                item.RoomsNo, item.BathroomsNo, item.BedsNo, item.MaxGuestsNo, item.SquareMeters,
                item.Price, item.Latitude, item.Longitude, item.IsWholeApartment,
                item.MovingInTime == null ? null : ((DateTime)item.MovingInTime).ToString("HH:mm"),
                item.MovingOutTime == null ? null : ((DateTime)item.MovingOutTime).ToString("HH:mm"),
                item.AdditionalInfo
                );

            await _repository.CreateAsync(accommodation);
        }

        public async Task DeletePostAsync(long id)
        {
            var accommodation = await _repository.GetByIdAsync(id);
            if(accommodation == null)
            {
                throw new NotFoundException(id, "Accommodation");
            }
            await _repository.DeleteAsync(accommodation);
        }

        public async Task<AccommodationInfoDTO> GetPostByIdAsync(long id)
        {
            
            var accommodation = await _repository.GetByIdAsync(id);
            if(accommodation!=null)
            {
                var accommodationDTO = new AccommodationInfoDTO
                {
                    Id = accommodation.Id,
                    Title = accommodation.Title,
                    Description = accommodation.Description,
                    OwnerId = accommodation.OwnerId,
                    Owner = accommodation.Owner,
                    DatePublished = accommodation.DatePublished,
                    CategoryId = accommodation.CategoryId,
                    Category = accommodation.Category,
                    Address = accommodation.Address,
                    ReferencePoint = accommodation.ReferencePoint,
                    ContactNumber = accommodation.ContactNumber,
                    RoomsNo = accommodation.RoomsNo,
                    BathroomsNo = accommodation.BathroomsNo,
                    BedsNo = accommodation.BedsNo,
                    MaxGuestsNo = accommodation.MaxGuestsNo,
                    SquareMeters = accommodation.SquareMeters,
                    Price = accommodation.Price,
                    Latitude = accommodation.Latitude,
                    Longitude = accommodation.Longitude,
                    IsWholeApartment = accommodation.IsWholeApartment,
                    MovingInTime = accommodation.MovingInTime,
                    MovingOutTime = accommodation.MovingOutTime,
                    AdditionalInfo = accommodation.AdditionalInfo,
                    AccommodationFacilities = accommodation.AccommodationFacilities,
                    AccommodationRules = accommodation.AccommodationRules,
                    AccommodationSpecificities = accommodation.AccommodationSpecificities,
                    AccommodationPhotos = accommodation.AccommodationPhotos

                };
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


            var accommodationToUpdate = new Accommodation(accommodation.Id,
                item.Title, item.Description, item.OwnerId, accommodation.DatePublished, item.CategoryId,
                item.Address, item.ReferencePoint, item.ContactNumber, item.RoomsNo, item.BathroomsNo,
                item.BedsNo, item.MaxGuestsNo, item.SquareMeters, item.Price, item.Latitude, item.Longitude,
                item.IsWholeApartment, item.MovingInTime == null ? null : ((DateTime)item.MovingInTime).ToString("HH:mm"),
                item.MovingOutTime == null ? null : ((DateTime)item.MovingOutTime).ToString("HH:mm"),
                item.AdditionalInfo);
            await _repository.UpdateAsync(accommodationToUpdate);
        }
    }
}
