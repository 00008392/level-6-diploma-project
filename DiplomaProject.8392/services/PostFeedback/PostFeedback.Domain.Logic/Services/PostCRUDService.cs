using AutoMapper;
using BaseClasses.Contracts;
using BaseClasses.Specifications;
using BaseClasses.Specifications.Composite;
using FluentValidation;
using PostFeedback.Domain.Entities;
using PostFeedback.Domain.Logic.Contracts;
using PostFeedback.Domain.Logic.DTOs;
using PostFeedback.Domain.Logic.Exceptions;
using PostFeedback.Domain.Logic.Filter;
using PostFeedback.Domain.Logic.Specifications;
using PostFeedback.Domain.Logic.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Services
{
    public class PostCRUDService : IPostCRUDService
    {
        private readonly IRepositoryWithIncludes<Post> _repository;
        private readonly IRepository<User> _ownerRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<City> _cityRepository;
        private readonly AbstractValidator<PostManipulationDTO> _validator;
        private readonly IMapper _mapper;
        public PostCRUDService(IRepositoryWithIncludes<Entities.Post> repository, 
            IRepository<User> ownerRepository,
            IRepository<Category> categoryRepository,
            IRepository<City> cityRepository,
            AbstractValidator<PostManipulationDTO> validator, 
            IMapper mapper)
        {
            _repository = repository;
            _ownerRepository = ownerRepository;
            _categoryRepository = categoryRepository;
            _cityRepository = cityRepository;
            _validator = validator;
            _mapper = mapper;
        }
        public async Task CreatePostAsync(PostManipulationDTO item)
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
            var post = _mapper.Map<Post>(item);

            await _repository.CreateAsync(post);
        }

        public async Task DeletePostAsync(long id)
        {
            var post = await _repository.GetByIdAsync(id, relatedEntitiesIncluded: true);

            if(post == null)
            {
                throw new NotFoundException(id, "Accommodation");
            }
            if (post.Bookings.Any())
            {
                throw new DeleteAccommodationException(id);
            }
            await _repository.DeleteAsync(post);
        }

        public async Task<ICollection<PostDetailsDTO>> GetAllPostsAsync(FilterParameters filter)
        {
            ICollection<Post> posts = new List<Post>();

            List<Specification<Post>> specifications = new();
            if(filter.SearchText!=null)
            {
                var specification = new SearchPostSpecification(filter.SearchText);
                specifications.Add(specification);
            }
            if (filter.Owner != null)
            {
                var specification = new PostsByUserSpecification((long)filter.Owner);
                specifications.Add(specification);
            }
            if (filter.Category != null)
            {
                var specification = new PostsByCategorySpecification((long)filter.Category);
                specifications.Add(specification);
            }
            if (filter.City != null)
            {
                var specification = new PostsByCitySpecification((long)filter.City);
                specifications.Add(specification);
            }
            if (filter.MinRooms != null||filter.MaxRooms!=null)
            {
                var specification = new RoomsNumberSpecification(filter.MinRooms, filter.MaxRooms);
                specifications.Add(specification);
            }
            if (filter.MinBeds != null || filter.MaxBeds != null)
            {
                var specification = new BedsNumberSpecification(filter.MinBeds, filter.MaxBeds);
                specifications.Add(specification);
            }
            if (filter.Guests != null)
            {
                var specification = new NumberOfGuestsSpecification((int)filter.Guests);
                specifications.Add(specification);
            }
            if (filter.MinPrice != null || filter.MaxPrice != null)
            {
                var specification = new PriceSpecification(filter.MinPrice, filter.MaxPrice);
                specifications.Add(specification);
            }
            if (filter.EntireApartment != null)
            {
                var specification = new IsWholeApartmentSpecification((bool)filter.EntireApartment);
                specifications.Add(specification);
            }
            CompositeSpecification<Post> compositeSpecification = null;
            if(specifications.Count>0)
            {
                compositeSpecification = new AndSpecification<Post>(null, specifications[0]);
                for (var i=0; i<specifications.Count-1; i++)
                {
                    compositeSpecification = new AndSpecification<Post>(compositeSpecification,
                        specifications[i + 1]);
                }
            }
           if(compositeSpecification!=null)
            {
                posts = await _repository
                    .GetFilteredAsync(compositeSpecification.ToExpression(), relatedEntitiesIncluded: true);
            }
            else
            {
                posts = await _repository
                    .GetAllAsync(relatedEntitiesIncluded: true);
            }

            return _mapper.Map<ICollection<PostDetailsDTO>>(posts);
        }

        public async Task<PostDetailsDTO> GetPostByIdAsync(long id)
        {
            
            var post = await _repository.GetByIdAsync(id, true);
            if(post != null)
            {
                var postDTO = _mapper.Map<PostDetailsDTO>(post);
                
                return postDTO;
            }
            return null;
        }

        public async Task UpdatePostAsync(PostManipulationDTO item)
        {
            var post = await _repository.GetByIdAsync(item.Id);
            if(post == null)
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
            if (item.OwnerId!= post.OwnerId)
            {
                throw new ForeignKeyViolationException("Owner");
            }
            post.UpdateInfo(
                item.Title, item.Description, item.OwnerId, item.CategoryId, item.CityId,
                item.Address, item.ReferencePoint, item.ContactNumber, item.RoomsNo, item.BathroomsNo,
                item.BedsNo, item.MaxGuestsNo, item.SquareMeters, item.Price,
                item.IsWholeApartment, item.MovingInTime == null ? null : ((DateTime)item.MovingInTime).ToString("HH:mm"),
                item.MovingOutTime == null ? null : ((DateTime)item.MovingOutTime).ToString("HH:mm"),
                item.AdditionalInfo);
            await _repository.UpdateAsync(post);
        }
    }
}
