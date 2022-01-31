using AutoMapper;
using BaseClasses.Contracts;
using PostFeedback.Domain.Entities;
using PostFeedback.Domain.Logic.Contracts;
using PostFeedback.Domain.Logic.DTOs;
using PostFeedback.Domain.Logic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Services
{
    public class PostItemsService<T, E> : IPostItemsService<T, E>
        where T : PostItem
        where E : Item

    {
        private readonly IRepository<T> _repository;
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<E> _itemRepository;
        private readonly IMapper _mapper;

        public PostItemsService(
            IRepository<T> repository,
            IRepository<Post> postRepository,
            IRepository<E> itemRepository,
            IMapper mapper)
        {
            _repository = repository;
            _postRepository = postRepository;
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public async Task AddItemsAsync(long postId, ICollection<AddItemToPostDTO> itemsList)
        {
            CheckItemsList(itemsList);
            var items = new List<T>();
            var duplicatesinList = (itemsList.Where(x => x.OtherValue != null).GroupBy(x => new {
                x.ItemId,
                x.OtherValue
            }).Any(g => g.Count() > 1)) || (itemsList.Where(x => x.OtherValue == null).GroupBy(x => new {
                x.ItemId
            }).Any(g => g.Count() > 1));
            if (duplicatesinList)
            {
                throw new DuplicateListValueException();
            }
            if (!_postRepository.DoesItemWithIdExist(postId))
            {
                throw new ForeignKeyViolationException(nameof(Post));
            }
            foreach (var itemDTO in itemsList)
            {

                if (!_itemRepository.DoesItemWithIdExist(itemDTO.ItemId))
                {
                    throw new ForeignKeyViolationException(nameof(Item));
                }
                var isOther = (await _itemRepository.GetFilteredAsync(i => i.Id == itemDTO.ItemId)).FirstOrDefault().IsOther;
                if (itemDTO.OtherValue != null)
                {
                    if (isOther == null)
                    {
                        throw new OtherItemException(itemDTO.ItemId);
                    }

                    await CheckDuplicates(i => i.PostId == postId
                    && i.ItemId == itemDTO.ItemId && i.OtherValue == itemDTO.OtherValue);
                }
                else
                {
                    if(isOther==true)
                    {
                        throw new OtherItemException();
                    }
                   await CheckDuplicates(i => i.PostId == postId
                   && i.ItemId == itemDTO.ItemId);
                }

                var item = (T)Activator.CreateInstance(typeof(T), postId, itemDTO.ItemId, itemDTO.OtherValue);
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

        public async Task RemoveItemsAsync(long postId, ICollection<long> itemsIds)
        {
            CheckItemsList(itemsIds);
            var ids = itemsIds.Distinct().ToList();
            var itemsRange = new List<T>();
            foreach (var id in ids)
            {
                var items = await _repository.GetFilteredAsync(x=>x.ItemId==id 
                && x.PostId == postId);
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
