
using EventBus.Contracts;
using ExceptionHandling;
using Post.Domain.Core;
using Post.Domain.Logic.Contracts;
using Post.Domain.Logic.DTOs;
using Post.Domain.Logic.IntegrationEvents.Events.Core;
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
        private readonly IPostRelatedInfoService<T, E> _service;
        private readonly IEventBus _eventBus;
        public PostRelatedInfoGenericStrategy(IPostRelatedInfoService<T, E> service,
                     IEventBus eventBus)
        {
            _service = service;
            _eventBus = eventBus;
        }
        public async Task<Response> AddItemsAsync(AddItemsRequest request, AccommodationItemAddedIntegrationEvent @event)
        {
            var items = new List<AccommodationItemDTO>();
            foreach (var item in request.Items)
            {
                var accommodationItemDTO = new AccommodationItemDTO(item.AccommodationId,
                    item.ItemId, item.OtherValue);
                items.Add(accommodationItemDTO);
            }
            var response = new Response();
            try
            {
                await _service.AddItemsAsync(items);

                response.IsSuccess = true;
                _eventBus.Publish(@event);
            }
            catch (Exception ex)
            {
                response.HandleException(ex);
            }
            return response;
        }

        public async Task<Response> RemoveItemsAsync(RemoveItemsRequest request, AccommodationItemRemovedIntegrationEvent @event)
        {
            var response = new Response();
            try
            {
                await _service.RemoveItemsAsync(request.Ids);
                response.IsSuccess = true;
                _eventBus.Publish(@event);

            }
            catch (Exception ex)
            {
                response.HandleException(ex);
            }
            return response;
        }
    }
}
