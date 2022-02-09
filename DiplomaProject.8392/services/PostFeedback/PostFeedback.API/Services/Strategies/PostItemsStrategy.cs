using AutoMapper;
using API.ExceptionHandling;
using PostFeedback.Domain.Entities;
using PostFeedback.Domain.Logic.Contracts;
using PostFeedback.Domain.Logic.DTOs;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostFeedback.API.Services.Strategies
{
    public class PostItemsStrategy<T, E> : IPostItemsStrategy<T, E>
        where T : Domain.Entities.PostItem
        where E : Domain.Entities.Item
    {
        private readonly IPostItemsService<T, E> _service;
        private readonly IMapper _mapper;
        public PostItemsStrategy(IPostItemsService<T, E> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        public async Task<Response> AddItemsAsync(AddItemsRequest request)
        {
            var addItems = _mapper.Map<ICollection<AddItemToPostDTO>>(request.Items);
            var response = new Response();
            try
            {
                await _service.AddItemsAsync(request.PostId, addItems);

                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.HandleException(ex);
            }
            return response;
        }

        public async Task<ICollection<Item>> GetItemsAsync()
        {
            return _mapper.Map<ICollection<ItemInfoDTO>, ICollection<Item>>
               (await _service.GetItemsAsync());
        }

        public async Task<Response> RemoveItemsAsync(RemoveItemsRequest request)
        {
            var response = new Response();
            try
            {
                await _service.RemoveItemsAsync(request.PostId, request.Items);
                response.IsSuccess = true;

            }
            catch (Exception ex)
            {
                response.HandleException(ex);
            }
            return response;
        }
    }
}
