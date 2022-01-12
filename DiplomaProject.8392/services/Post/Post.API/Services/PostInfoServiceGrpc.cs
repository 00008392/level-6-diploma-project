using AutoMapper;
using Grpc.Core;
using Post.Domain.Logic.Contracts;
using Post.Domain.Logic.DTOs;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post.API.Services
{
    public class PostInfoServiceGrpc: PostInfo.PostInfoBase
    {
        private readonly IInfoService _service;
        private readonly IMapper _mapper;
        public PostInfoServiceGrpc(IInfoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        public override async Task<CityList> GetAllCities(Empty request,
         ServerCallContext context)
        {
            return await GetItems<CityList, CityDTO, City>
               (_service.GetAllCitiesAsync);
        }
        public override async Task<CategoryList> GetAllCategories(Empty request,
        ServerCallContext context)
        {
            return await GetItems<CategoryList, CategoryDTO, Category>
               (_service.GetAllCategoriesAsync);
        }
        //T - object with list of items
        //D - DTO from domain logic layer
        //I - item which is in list
        private async Task<T> GetItems<T, D, I>(Func<Task<ICollection<D>>> action)
            where T : IItemList<I>, new()
        {
            var items = await action();
            var itemList = _mapper.Map<ICollection<D>, ICollection<I>>(items);
            var response = new T();
            response.Items.Add(itemList);
            return response;
        }
    }
}
