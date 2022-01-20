using AutoMapper;
using BaseClasses.Contracts;
using Post.Domain.Core;
using Post.Domain.Entities;
using Post.Domain.Logic.Contracts;
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
    public class AccommodationItemsService<T, E> : IAcommodationItemsService<T, E>
        where T : ItemAccommodationBase
        where E : ItemBase

    {
        private readonly IRepository<T> _repository;
        private readonly IRepository<Accommodation> _accommodationRepository;
        private readonly IRepository<E> _itemRepository;
        private readonly IMapper _mapper;

        public AccommodationItemsService(
            IRepository<T> repository,
            IRepository<Accommodation> accommodationRepository,
            IRepository<E> itemRepository,
            IMapper mapper)
        {
            _repository = repository;
            _accommodationRepository = accommodationRepository;
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public async Task AddItemsAsync(AddItemsDTO itemsDTO)
        {
            var itemsList = itemsDTO.Items;
            CheckItemsList(itemsList);
            var items = new List<T>();
            var duplicatesinList = (itemsList.Where(x => x.OtherItem != null).GroupBy(x => new {
                x.ItemId,
                x.OtherItem
            }).Any(g => g.Count() > 1)) || (itemsList.Where(x => x.OtherItem == null).GroupBy(x => new {
                x.ItemId
            }).Any(g => g.Count() > 1));
            if (duplicatesinList)
            {
                throw new DuplicateListValueException();
            }
            if (!_accommodationRepository.DoesItemWithIdExist(itemsDTO.AccommodationId))
            {
                throw new ForeignKeyViolationException("Accommodation");
            }
            foreach (var itemDTO in itemsList)
            {

                if (!_itemRepository.DoesItemWithIdExist(itemDTO.ItemId))
                {
                    throw new ForeignKeyViolationException("Item");
                }
                var isOther = (await _itemRepository.GetFilteredAsync(i => i.Id == itemDTO.ItemId)).FirstOrDefault().IsOther;
                if (itemDTO.OtherItem != null)
                {
                    if (isOther == null)
                    {
                        throw new OtherItemException(itemDTO.ItemId);
                    }

                    await CheckDuplicates(i => i.AccommodationId == itemsDTO.AccommodationId
                    && i.ItemId == itemDTO.ItemId && i.OtherItem == itemDTO.OtherItem);
                }
                else
                {
                    if(isOther==true)
                    {
                        throw new OtherItemException();
                    }
                    await CheckDuplicates(i => i.AccommodationId == itemsDTO.AccommodationId
                   && i.ItemId == itemDTO.ItemId);
                }

                var item = (T)Activator.CreateInstance(typeof(T), itemsDTO.AccommodationId, itemDTO.ItemId, itemDTO.OtherItem);
                items.Add(item);
            }


            await _repository.AddRangeAsync(items);
        }

        public async Task<ICollection<ItemInfoDTO>> GetItemsAsync()
        {
            var items = await _itemRepository.GetAllAsync();
            if (items == null)
            {
                return null;
            }
            var itemDTOs = _mapper.Map<ICollection<E>, ICollection<ItemInfoDTO>>(items);
            return itemDTOs;
        }

        public async Task RemoveItemsAsync(RemoveItemsDTO itemsDTO)
        {
            CheckItemsList(itemsDTO.Items);
            var ids = itemsDTO.Items.Distinct().ToList();
            var itemsRange = new List<T>();
            foreach (var id in ids)
            {
                var items = await _repository.GetFilteredAsync(x=>x.ItemId==id 
                && x.AccommodationId == itemsDTO.AccommodationId);
                if (items.Count==0)
                {
                    throw new NotFoundException(id, "Item");
                }
                itemsRange.AddRange(items);
            }

            await _repository.RemoveRangeAsync(itemsRange);
        }
        private void CheckItemsList<I>(ICollection<I> items)
        {
            if (items.Count == 0)
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
