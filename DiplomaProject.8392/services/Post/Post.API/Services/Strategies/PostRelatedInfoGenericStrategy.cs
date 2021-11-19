
using AutoMapper;
using EventBus.Contracts;
using ExceptionHandling;
using Post.Domain.Core;
using Post.Domain.Logic.Contracts;
using Post.Domain.Logic.DTOs;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post.API.Services.Strategies
{
    public class PostRelatedInfoGenericStrategy<T, E> : IPostRelatedInfoStrategy<T, E>
        where T : ItemAccommodationBase
        where E : ItemBase
    {
        private readonly IPostItemsmanipulationService<T, E> _service;
        private readonly IMapper _mapper;
        public PostRelatedInfoGenericStrategy(IPostItemsmanipulationService<T, E> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        public async Task<Response> AddItemsAsync(AddItemsRequest request)
        {
            var items = _mapper.Map<ICollection<ItemRequest>, ICollection<AccommodationItemDTO>>(request.Items);
            var response = new Response();
            try
            {
                await _service.AddItemsAsync(items);

                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.HandleException(ex);
            }
            return response;
        }

        public async Task<Response> RemoveItemsAsync(RemoveItemsRequest request)
        {
            var response = new Response();
            try
            {
                await _service.RemoveItemsAsync(request.Ids);
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
