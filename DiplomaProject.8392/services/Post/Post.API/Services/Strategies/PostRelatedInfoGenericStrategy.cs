using Post.API.ExceptionHandling;
using Post.Domain.Core;
using Post.Domain.Logic.Contracts;
using Post.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post.API.Services.Strategies
{
    public class PostRelatedInfoGenericStrategy<T, E> : IPostRelatedInfoStrategy<T, E>
        where T : ItemAccommodationBase, new()
        where E : ItemBase
    {
        private readonly IPostRelatedInfoService<T, E> _service;
        public PostRelatedInfoGenericStrategy (IPostRelatedInfoService<T, E> service)
        {
            _service = service;
        }
        public async Task<Response> AddItemsAsync(AddItemsRequest request)
        {
            var items = new List<AccommodationItemDTO>();
            foreach (var item in request.Items)
            {
                var accommodationItemDTO = new AccommodationItemDTO
                {
                    AccommodationId = item.AccommodationId,
                    ItemId = item.ItemId,
                    OtherItem = item.OtherValue
                };
                items.Add(accommodationItemDTO);
            }
          
            try
            {
                await _service.AddItemsAsync(items);
                return new Response
                {
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return ExceptionHandler.HandleException(ex);
            }
        }

        public async Task<Response> RemoveItemsAsync(RemoveItemsRequest request)
        {
            try
            {
                await _service.RemoveItemsAsync(request.Ids);
                return new Response
                {
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return ExceptionHandler.HandleException(ex);
            }
        }
    }
}
