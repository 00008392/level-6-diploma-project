using Account.Domain.Logic.Contracts;
using Account.Domain.Logic.DTOs;
using AutoMapper;
using Grpc.Core;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Helpers;

namespace Account.API.Services
{
    //grpc service for retrieving entities related to user and necessary for user creation and modification
    public class UserRelatedInfoServiceGrpc : UserRelatedInfoService.UserRelatedInfoServiceBase
    {
        //inject service from domain logic layer
        private readonly IUserRelatedInfoService _service;
        private readonly IMapper _mapper;
        public UserRelatedInfoServiceGrpc(
            IUserRelatedInfoService service,
            IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        //retrieve list of countries
        public override async Task<CountryList> GetAllCountries(Empty request,
          ServerCallContext context)
        {
            //call helper method that handles retrieval of items and maps them to grpc response
            return await GrpcServiceHelper.GetItemsAsync<CountryList, CountryDTO, Country>
               (_service.GetAllCountriesAsync, _mapper);
        }

    }
}
