using AutoMapper;
using EventBus.Contracts;
using EventBus.Events;
using FluentValidation;
using PostFeedback.Domain.Entities;
using PostFeedback.Domain.Logic.Contracts;
using PostFeedback.Domain.Logic.DTOs;
using PostFeedback.Domain.Logic.Exceptions;
using PostFeedback.Domain.Logic.Filter;
using PostFeedback.Domain.Logic.IntegrationEvents.Events;
using PostFeedback.Domain.Logic.Specifications;
using PostFeedback.Domain.Logic.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using DAL.Base.Contracts;
using Domain.Logic.Base.Exceptions;
using Domain.Logic.Base.Specifications;
using Domain.Logic.Base.Helpers;

namespace PostFeedback.Domain.Logic.Services
{
    //Post CRUD service
    public class PostService : IPostService
    {
        private readonly IRepository<Post> _repository;
        private readonly IRepository<User> _ownerRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<Rule> _ruleRepository;
        private readonly IRepository<Facility> _facilityRepository;
        private readonly AbstractValidator<PostManipulationDTO> _validator;
        private readonly IMapper _mapper;
        private readonly IEventBus _eventBus;
        public PostService(IRepository<Post> repository,
            IRepository<User> ownerRepository,
            IRepository<Category> categoryRepository,
            IRepository<City> cityRepository,
            IRepository<Rule> ruleRepository,
            IRepository<Facility> facilityRepository,
            AbstractValidator<PostManipulationDTO> validator,
            IMapper mapper,
            IEventBus eventBus)
        {
            _repository = repository;
            _ownerRepository = ownerRepository;
            _categoryRepository = categoryRepository;
            _cityRepository = cityRepository;
            _ruleRepository = ruleRepository;
            _facilityRepository = facilityRepository;
            _validator = validator;
            _mapper = mapper;
            _eventBus = eventBus;
        }
        //this method creates new post
        public async Task CreatePostAsync(PostManipulationDTO item)
        {
            await HandlePostAsync(item, _repository.CreateAsync,
                editMode: false);
        }
        //this method deletes post
        public async Task DeletePostAsync(long id)
        {
            //check if post exists in the database
            //if not, throw exception
            var post = await _repository.GetByIdAsync(id, x=>x.Bookings);
            if (post == null)
            {
                throw new NotFoundException(id, nameof(Post));
            }
            //if post has active bookings (not expired), it cannot be deleted
            if (post.Bookings?.Any(x => x.EndDate > DateTime.Now)??false)
            {
                throw new DeletePostException(id);
            }
            //delete post
            await _repository.DeleteAsync(id);
            //if post is deleted successfully and no exception is thrown, 
            //publish integration event
            var integrationEvent = new PostDeletedIntegrationEvent(id);
            _eventBus.Publish(integrationEvent);
        }
        //this method retrieves posts by specified filter criterias
        public async Task<ICollection<PostDetailsDTO>> GetPostsAsync(FilterParameters filter)
        {
            ICollection<Post> posts = new List<Post>();
            //based on filter criteria that are present, specifications are created 
            //and composite specification is formed from list of specifications
            List<Specification<Post>> specifications = new();
            //filter posts by search input if present
            if (filter.SearchText != null)
            {
                var specification = new SearchPostSpecification(filter.SearchText);
                specifications.Add(specification);
            }
            //filter posts by owner id if present
            if (filter.Owner != null)
            {
                var specification = new PostsByUserSpecification((long)filter.Owner);
                specifications.Add(specification);
            }
            //filter posts by category if present
            if (filter.Category != null)
            {
                var specification = new PostsByCategorySpecification((long)filter.Category);
                specifications.Add(specification);
            }
            //filter posts by city if present
            if (filter.City != null)
            {
                var specification = new PostsByCitySpecification((long)filter.City);
                specifications.Add(specification);
            }
            //filter posts by number of rooms range if present
            if (filter.MinRooms != null || filter.MaxRooms != null)
            {
                var specification = new RoomsNumberSpecification(filter.MinRooms, filter.MaxRooms);
                specifications.Add(specification);
            }
            //filter posts by number of beds range if present
            if (filter.MinBeds != null || filter.MaxBeds != null)
            {
                var specification = new BedsNumberSpecification(filter.MinBeds, filter.MaxBeds);
                specifications.Add(specification);
            }
            //filter posts by number of guests if present
            if (filter.Guests != null)
            {
                var specification = new NumberOfGuestsSpecification((int)filter.Guests);
                specifications.Add(specification);
            }
            //filter posts by price range if present
            if (filter.MinPrice != null || filter.MaxPrice != null)
            {
                var specification = new PriceSpecification(filter.MinPrice, filter.MaxPrice);
                specifications.Add(specification);
            }
            //filter posts by availability type (entire accommodation or not) if present
            if (filter.EntireApartment != null)
            {
                var specification = new IsWholeApartmentSpecification((bool)filter.EntireApartment);
                specifications.Add(specification);
            }
            //filter posts by availability in range of dates 
            if(filter.StartDate!=null&&filter.EndDate!=null)
            {
                var specification = new DatesBookedSpecification((DateTime)filter.StartDate,
                    (DateTime)filter.EndDate);
                specifications.Add(specification);
            }
            CompositeSpecification<Post> compositeSpecification = null;
            //form composite specification if there are any specifications created
            if (specifications.Count > 0)
            {
                compositeSpecification = new AndSpecification<Post>(null, specifications[0]);
                //chain specifications into composite one by adding specifications one by one to 
                //existing composite specification
                for (var i = 0; i < specifications.Count - 1; i++)
                {
                    compositeSpecification = new AndSpecification<Post>(compositeSpecification,
                        specifications[i + 1]);
                }
            }
            Expression<Func<Post, object>>[] includes = { x => x.Category, x => x.City, x => x.Bookings, x=>x.Owner };
            //if specifications are present, filter posts 
            if (compositeSpecification != null)
            {
                posts = await _repository
                    .GetFilteredAsync(compositeSpecification.ToExpression(), 
                    includes);
            }
            else
            {
                //if specifications are not present, get all posts
                posts = await _repository
                    .GetAllAsync(includes);
            }
            //map entities to dtos
            return _mapper.Map<ICollection<PostDetailsDTO>>(posts);
        }
        //this method retrieves post by id
        public async Task<PostDetailsDTO> GetPostByIdAsync(long id)
        {
            //get post by id including related entities
            var post = await _repository.GetByIdAsync(id, x=>x.Category, x=>x.City,
                x=>x.Owner, x=>x.Rules, x=>x.Facilities, x=>x.Photos, x=>x.Bookings);
            //map to dto if post exists
            if (post != null)
            {
                var postDTO = _mapper.Map<PostDetailsDTO>(post);

                return postDTO;
            }
            return null;
        }
        //this method updates existing post
        public async Task UpdatePostAsync(PostManipulationDTO item)
        {
            await HandlePostAsync(item, _repository.UpdateAsync,
                editMode: true);
        }
        //method that adds collection of items to post by their ids
        //and throws exception in case of attempt to add non-existing item
        private async Task AddItemsToPostAsync<T>(
            ICollection<long> itemIds,
            Action<ICollection<T>> addAction,
            IRepository<T> repository) where T: Item
        {
            //distinct is used to avoid duplicates in collection
            itemIds = itemIds.Distinct().ToList();
            //list of items that will be added to post
            var itemsToAdd = new List<T>();
            //check if each item exists in the database and retrieve it
            foreach (var itemId in itemIds)
                {
                    //check that item exists and throw exception if not
                    var item = await repository.GetByIdAsync(itemId);
                    if (item == null)
                    {
                        throw new ForeignKeyViolationException($"{typeof(T).Name}");
                    }
                //add to the list 
                itemsToAdd.Add(item);
                }
            //attach collection to post
            addAction(itemsToAdd);
        }
        //method that handles creation and modification of post
        private async Task HandlePostAsync(
            PostManipulationDTO item,
            Func<Post, Task> repositoryAction,
            bool editMode)
        {
            //if post is getting updated, then it is necessary to check if post exists
            if(editMode)
            {
                //check if post exists in the database and throw exception if not found
                if (!_repository.DoesItemWithIdExist(item.Id))
                {
                    throw new NotFoundException(item.Id, nameof(Post));
                }
            }
            //validate post and throw exception in case validation fails
            ServiceHelper.ValidateItem(_validator, item);
            //check that category (if any) attached to post exists in the database
            //and throw exception if not
            if (item.CategoryId != null)
            {
                ServiceHelper.CheckIfRelatedEntityExists((long)item.CategoryId, _categoryRepository);
            }
            //check that city attached to post exists in the database
            //and throw exception if not
            ServiceHelper.CheckIfRelatedEntityExists(item.CityId, _cityRepository);
            //check that owner attached to post exists in the database
            //and throw exception if not
            ServiceHelper.CheckIfRelatedEntityExists(item.OwnerId, _ownerRepository);
            //map dto to post entity
            var post = _mapper.Map<Post>(item);
            //add rules to post
            await AddItemsToPostAsync(item.Rules, post.SetRules, _ruleRepository);
            //add facilities to post
            await AddItemsToPostAsync(item.Facilities, post.SetFacilities, _facilityRepository);
            //either create or update post
            await repositoryAction(post);
            //if post is handled successfully and no exception is thrown, 
            //publish integration event 
            var integrationEvent = _mapper.Map<PostCreatedOrUpdatedIntegrationEvent>(post);
            _eventBus.Publish(integrationEvent);
        }
    }
}
