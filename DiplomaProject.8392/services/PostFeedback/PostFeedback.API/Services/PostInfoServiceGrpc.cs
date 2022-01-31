using AutoMapper;
using Grpc.Core;
using PostFeedback.Domain.Logic.Contracts;
using PostFeedback.Domain.Logic.DTOs;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostFeedback.API.Services
{
    public class PostInfoServiceGrpc : PostInfo.PostInfoBase
    {
        private readonly IInfoService _service;
        private readonly IMapper _mapper;
        public PostInfoServiceGrpc(IInfoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        public override async Task<CategoryCityList> GetAllCities(Empty request,
         ServerCallContext context)
        {
            return await GetItems(_service.GetAllCitiesAsync);
        }
        public override async Task<CategoryCityList> GetAllCategories(Empty request,
        ServerCallContext context)
        {
            return await GetItems(_service.GetAllCategoriesAsync);
        }
        private async Task<CategoryCityList> GetItems(Func<Task<ICollection<CategoryCityDTO>>> action)
        {
            var items = await action();
            var itemList = _mapper.Map<ICollection<CategoryCityDTO>, ICollection<CategoryCity>>(items);
            var response = new CategoryCityList();
            response.Items.Add(itemList);
            return response;
        }
    }
}
