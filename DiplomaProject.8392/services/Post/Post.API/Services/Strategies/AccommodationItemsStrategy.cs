using AutoMapper;
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
    public class AccommodationItemsStrategy<T, E>: IAccommodationItemsStrategy<T, E>
        where T : ItemAccommodationBase
        where E : ItemBase
    {
        private readonly IAcommodationItemsService<T, E> _service;
        private readonly IMapper _mapper;
        public AccommodationItemsStrategy(IAcommodationItemsService<T, E> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        public async Task<Response> AddItemsAsync(AddItemsRequest request)
        {
            var addItems = _mapper.Map<AddItemsDTO>(request);
            var response = new Response();
            try
            {
                await _service.AddItemsAsync(addItems);

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
                var removeItems = _mapper.Map<RemoveItemsDTO>(request);
                await _service.RemoveItemsAsync(removeItems);
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
