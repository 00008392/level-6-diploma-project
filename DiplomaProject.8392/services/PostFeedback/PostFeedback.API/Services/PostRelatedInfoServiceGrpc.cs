using AutoMapper;
using Grpc.Base.Helpers;
using Grpc.Core;
using PostFeedback.Domain.Entities;
using PostFeedback.Domain.Logic.Contracts;
using PostFeedback.Domain.Logic.DTOs;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostFeedback.API.Services
{
    //grpc service for retrieving entities related to post and necessary for post creation and modification
    public class PostRelatedInfoServiceGrpc : PostRelatedInfoService.PostRelatedInfoServiceBase
    {
        //inject services from domain logic layer
        private readonly IPostRelatedInfoService<Category> _categoryService;
        private readonly IPostRelatedInfoService<City> _cityService;
        private readonly IPostRelatedInfoService<Rule> _ruleService;
        private readonly IPostRelatedInfoService<Facility> _facilityService;
        private readonly IMapper _mapper;

        public PostRelatedInfoServiceGrpc(
            IPostRelatedInfoService<Category> categoryService,
            IPostRelatedInfoService<City> cityService,
            IPostRelatedInfoService<Rule> ruleService,
            IPostRelatedInfoService<Facility> facilityService,
            IMapper mapper)
        {
            _categoryService = categoryService;
            _cityService = cityService;
            _ruleService = ruleService;
            _facilityService = facilityService;
            _mapper = mapper;
        }
        //retrieve list of cities
        public override async Task<ItemListResponse> GetAllCities(Empty request,
         ServerCallContext context)
        {
            return await HandleItemsAsync(_cityService);
        }
        //retrieve list of categories
        public override async Task<ItemListResponse> GetAllCategories(Empty request,
        ServerCallContext context)
        {
            return await HandleItemsAsync(_categoryService);
        }
        //retrieve list of rules
        public override async Task<ItemListResponse> GetAllRules(Empty request,
        ServerCallContext context)
        {
            return await HandleItemsAsync(_ruleService);
        }
        //retrieve list of facilities
        public override async Task<ItemListResponse> GetAllFacilities(Empty request,
        ServerCallContext context)
        {
            return await HandleItemsAsync(_facilityService);
        }
        //method common for retrieval of all items
        private async Task<ItemListResponse> HandleItemsAsync<T>(IPostRelatedInfoService<T> service)
            where T: Domain.Entities.Item
        {
            //call helper method that handles item retrieval and maps it to grpc response
            return await GrpcServiceHelper.
             GetItemsAsync<ItemListResponse, ItemDTO, Item>(service.GetAllItemsAsync, _mapper);
        }
    }
}
