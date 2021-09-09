using Post.Domain.Core;
using Post.Domain.Logic.IntegrationEvents.Events.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post.API.Services.Strategies
{
    public interface IPostRelatedInfoStrategy<T, E> 
        where T: ItemAccommodationBase
        where E: ItemBase
    {
        Task<Response> AddItemsAsync(AddItemsRequest request, AccommodationItemAddedIntegrationEvent @event);
        Task<Response> RemoveItemsAsync(RemoveItemsRequest request, AccommodationItemRemovedIntegrationEvent @event);
    }
}
