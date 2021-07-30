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
    public class PostRelatedInfoService<T, E> : IPostRelatedInfoService<T, E> 
                                                                              where T: ItemAccommodationBase, new()
                                                                              where E: ItemBase

    {
        private readonly IRepository<T> _repository;
        private readonly IRepository<Accommodation> _accommodationRepository;
        private readonly IRepository<E> _itemRepository;
        public PostRelatedInfoService(IRepository<T> repository, IRepository<Accommodation> accommodationRepository,
            IRepository<E> itemRepository)
        {
            _repository = repository;
            _accommodationRepository = accommodationRepository;
            _itemRepository = itemRepository;
        }

        public async Task AddItemAsync(AccommodationItemDTO itemDTO)
        {

            if(!_accommodationRepository.IfExists(itemDTO.AccommodationId))
            {
                throw new ForeignKeyViolationException("Accommodation");
            }
            if(!_itemRepository.IfExists(itemDTO.ItemId))
            {
                throw new ForeignKeyViolationException("Item");
            }
            var item = new T
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
