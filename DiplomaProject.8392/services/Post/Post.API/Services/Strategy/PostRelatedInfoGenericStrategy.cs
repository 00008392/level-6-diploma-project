using Post.API.ExceptionHandling;
using Post.Domain.Core;
using Post.Domain.Logic.Contracts;
using Post.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post.API.Services.Strategy
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
        public async Task<Response> AddItemAsync(AddItemRequest request)
        {
            var accommodationItemDTO = new AccommodationItemDTO
            {
                AccommodationId = request.AccommodationId,
                ItemId = request.ItemId,
                OtherItem = request.OtherValue
            };
            try
            {
                await _service.AddItemAsync(accommodationItemDTO);
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

        public async Task<Response> RemoveItemAsync(Request request)
        {
            try
            {
                await _service.RemoveItemAsync(request.Id);
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
