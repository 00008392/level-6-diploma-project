using FluentValidation;
using Post.Domain.Core;
using Post.Domain.Entities;
using Post.Domain.Logic.Contracts;
using Post.Domain.Logic.Core;
using Post.Domain.Logic.DTOs;
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
        private readonly IRepository<Accommodation> _repository;
        private readonly IRepository<Owner> _ownerRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly AbstractValidator<BaseAccommodationDTO> _validator;
        public PostCRUDService(IRepository<Accommodation> repository, 
            IRepository<Owner> ownerRepository,
            IRepository<Category> categoryRepository,
            AbstractValidator<BaseAccommodationDTO> validator)
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
                var categoryExists = _categoryRepository.IfExists((long)item.CategoryId);
                if (!categoryExists)
                {
                    throw new Exception("Invalid category");
                }
            }
            var ownerExists = _ownerRepository.IfExists(item.OwnerId);
            if (!ownerExists)
            {
                throw new Exception("Invalid owner");
            }
            var accommodation = new Accommodation
            {
                Title = item.Title,
                Description = item.Description,
                OwnerId = item.OwnerId,
                DatePublished = DateTime.Now,
                CategoryId = item.CategoryId,
                Address = item.Address,
                ReferencePoint = item.ReferencePoint,
                ContactNumber = item.ContactNumber,
                RoomsNo = item.RoomsNo,
                BathroomsNo = item.BathroomsNo,
                BedsNo = item.BedsNo,
                MaxGuestsNo = item.MaxGuestsNo,
                SquareMeters = item.MaxGuestsNo,
                Price = item.Price,
                Latitude = item.Latitude,
                Longitude = item.Longitude,
                IsWholeApartment = item.IsWholeApartment,
                AdditionalInfo = item.AdditionalInfo

            };
            await _repository.CreateAsync(accommodation);
        }

        public async Task DeletePostAsync(long id)
        {
            var accommodation = await _repository.GetByIdAsync(id);
            if(accommodation == null)
            {
                throw new Exception("Accommodation does not exist");
            }
            await _repository.DeleteAsync(accommodation);
        }

        public async Task<AccommodationInfoDTO> GetPostByIdAsync(long id)
        {
            
            var accommodation = await _repository.GetByIdAsync(id, a=>a.Owner, a=>a.Category, 
                a=>a.AccommodationFacilities, a=>a.AccommodationRules, a=>a.AccommodationSpecificities,
                a=>a.AccommodationPhotos);
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
                    SquareMeters = accommodation.MaxGuestsNo,
                    Price = accommodation.Price,
                    Latitude = accommodation.Latitude,
                    Longitude = accommodation.Longitude,
                    IsWholeApartment = accommodation.IsWholeApartment,
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
                throw new Exception("Accommodation does not exist");
            } 
            var validationResult = await _validator.ValidateAsync(item);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            if (item.CategoryId != null)
            {
                var categoryExists = _categoryRepository.IfExists((long)item.CategoryId);
                if (!categoryExists)
                {
                    throw new Exception("Invalid category");
                }
            } 
            if (item.OwnerId!=accommodation.OwnerId)
            {
                throw new Exception("Invalid owner");
            }

            accommodation.Title = accommodation.Title;
            accommodation.Description = accommodation.Description;
            accommodation.OwnerId = accommodation.OwnerId;
            accommodation.CategoryId = accommodation.CategoryId;
            accommodation.Address = accommodation.Address;
            accommodation.ReferencePoint = accommodation.ReferencePoint;
            accommodation.ContactNumber = accommodation.ContactNumber;
            accommodation.RoomsNo = accommodation.RoomsNo;
            accommodation.BathroomsNo = accommodation.BathroomsNo;
            accommodation.BedsNo = accommodation.BedsNo;
            accommodation.MaxGuestsNo = accommodation.MaxGuestsNo;
            accommodation.SquareMeters = accommodation.MaxGuestsNo;
            accommodation.Price = accommodation.Price;
            accommodation.Latitude = accommodation.Latitude;
            accommodation.Longitude = accommodation.Longitude;
            accommodation.IsWholeApartment = accommodation.IsWholeApartment;
            accommodation.AdditionalInfo = accommodation.AdditionalInfo;

            await _repository.UpdateAsync(accommodation);
        }
    }
}
