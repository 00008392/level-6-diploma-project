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

        public async Task AddItemsAsync(ICollection<AccommodationItemDTO> itemDTOs)
        {
            var items = new List<T>();
            foreach(var itemDTO in itemDTOs)
            {
                if (!_accommodationRepository.IfExists(itemDTO.AccommodationId))
                {
                    throw new ForeignKeyViolationException("Accommodation");
                }
                if (!_itemRepository.IfExists(itemDTO.ItemId))
                {
                    throw new ForeignKeyViolationException("Item");
                }
                var item = new T
                {
                    ItemId = itemDTO.ItemId,
                    AccommodationId = itemDTO.AccommodationId,
                    OtherItem = itemDTO.OtherItem
                };
                items.Add(item);
            }

          
            await _repository.AddRangeAsync(items);
        }

        public async Task RemoveItemsAsync(ICollection<long> ids)
        {
            var items = new List<T>();
            foreach(var id in ids)
            {
                var item = await _repository.GetByIdAsync(id);
                if (item == null)
                {
                    throw new NotFoundException(id, "Item");
                }
                items.Add(item);
            }
           
           await _repository.RemoveRangeAsync(items);
        }
    }
}
