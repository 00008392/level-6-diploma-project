using BaseClasses.Contracts;
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
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Post.Domain.Logic.Services
{
    //E - entity representing bridge table
    public class PostRelatedInfoService<T, E> : IPostRelatedInfoService<T, E> 
                                                                              where T: ItemAccommodationBase
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
            CheckItemsList(itemDTOs);
            var items = new List<T>();
            var duplicatesinList = (itemDTOs.Where(x=>x.OtherItem!=null).GroupBy(x=> new { 
            x.AccommodationId,
            x.ItemId,
            x.OtherItem
            }).Any(g => g.Count() > 1)) || (itemDTOs.Where(x => x.OtherItem == null).GroupBy(x => new {
                x.AccommodationId,
                x.ItemId
            }).Any(g => g.Count() > 1));
            if(duplicatesinList)
            {
                throw new DuplicateListValueException();
            }

            foreach (var itemDTO in itemDTOs)
            {

                if (!_accommodationRepository.DoesItemWithIdExist(itemDTO.AccommodationId))
                {
                    throw new ForeignKeyViolationException("Accommodation");
                }
                if (!_itemRepository.DoesItemWithIdExist(itemDTO.ItemId))
                {
                    throw new ForeignKeyViolationException("Item");
                }
                if(itemDTO.OtherItem!=null)
                {
                    var otherItem = (await _itemRepository.GetFilteredAsync(i => i.Id == itemDTO.ItemId && (bool)i.IsOther)).FirstOrDefault();
                    if(otherItem == null)
                    {
                        throw new OtherItemException();
                    }
                    
                    await CheckDuplicates(i => i.AccommodationId == itemDTO.AccommodationId
                    && i.ItemId == itemDTO.ItemId && i.OtherItem == itemDTO.OtherItem);
                }
                else
                {
                   
                    await CheckDuplicates(i => i.AccommodationId == itemDTO.AccommodationId
                   && i.ItemId == itemDTO.ItemId);
                }

                var item = (T)Activator.CreateInstance(typeof(T), itemDTO.ItemId, itemDTO.AccommodationId, itemDTO.OtherItem);
                items.Add(item);
            }

          
            await _repository.AddRangeAsync(items);
        }

        public async Task RemoveItemsAsync(ICollection<long> ids)
        {
            CheckItemsList(ids);
            if( ids.GroupBy(x => x).Any(g => g.Count() > 1))
            {
                throw new DuplicateListValueException();
            }
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

        private void CheckItemsList<I>(ICollection<I> items)
        {
            if(items.Count==0)
            {
                throw new EmptyItemsListException();
            }
        }
        private async Task CheckDuplicates(Expression<Func<T, bool>> filter)
        {
            var record = (await _repository.GetFilteredAsync(filter)).FirstOrDefault();
            if (record != null)
            {
                throw new DuplicateItemException(record.ItemId);
            }
        }
        
       
    }
}
