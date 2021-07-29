using FluentValidation;
using Post.Domain.Core;
using Post.Domain.Entities;
using Post.Domain.Logic.Contracts;
using Post.Domain.Logic.Core;
using Post.Domain.Logic.DTOs;
using Post.Domain.Logic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Post.Domain.Logic.Services
{
    //E - entity representing bridge table
    public class PostRelatedInfoService<T, E, B> : IPostRelatedInfoService<T, E, B> where T: AccommodationItemDTO
                                                                              where E: ItemAccommodationBase, new()
                                                                              where B: ItemBase

    {
        private readonly IRepository<E> _repository;
        private readonly IRepository<Accommodation> _accommodationRepository;
        private readonly IRepository<B> _itemRepository;
        private readonly AbstractValidator<T> _validator;
        public PostRelatedInfoService(IRepository<E> repository, IRepository<Accommodation> accommodationRepository,
            IRepository<B> itemRepository,
            AbstractValidator<T> validator)
        {
            _repository = repository;
            _accommodationRepository = accommodationRepository;
            _itemRepository = itemRepository;
            _validator = validator;
        }

        public async Task AddItemAsync(T itemDTO)
        {
            var validationResult = await _validator.ValidateAsync(itemDTO);
            if(!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            if(!_accommodationRepository.IfExists(itemDTO.AccommodationId))
            {
                throw new ForeignKeyViolationException("Accommodation");
            }
            if(!_itemRepository.IfExists(itemDTO.ItemId))
            {
                throw new ForeignKeyViolationException("Item");
            }
            var item = new E
            {
                ItemId = itemDTO.ItemId,
                AccommodationId = itemDTO.AccommodationId,
                OtherItem = itemDTO.OtherItem
            };
            await _repository.CreateAsync(item);
        }

        public async Task RemoveItemAsync(long id)
        {
            var item = await _repository.GetByIdAsync(id);
            if(item==null)
            {
                throw new NotFoundException(id, "Item");
            }
           await _repository.DeleteAsync(item);
        }
    }
}
