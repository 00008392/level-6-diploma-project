using Account.Domain.Logic.Contracts;
using Account.Domain.Logic.DTOs;
using AutoMapper;
using Grpc.Core;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account.API.Services
{
    public class UserInfoServiceGrpc : UserInfo.UserInfoBase
    {
        private readonly IInfoService _service;
        private readonly IMapper _mapper;
        public UserInfoServiceGrpc(IInfoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        public override async Task<UserInfoResponse> GetUserInfo(Request request, 
            ServerCallContext context)
        {
            var user = await _service.GetProfileInfoAsync(request.Id);
            if (user == null)
            {
                return new UserInfoResponse
                {
                    NoUser = true
                };
            }
            var response = _mapper.Map<UserInfoResponse>(user);

            return response;
        }
        public override async Task<UserList> GetAllUsers(Empty request,
           ServerCallContext context)
        {
            return await GetItems<UserList, UserInfoDTO, UserInfoResponse>
                (_service.GetAllUsersAsync);
        }
        public override async Task<CountryList> GetAllCountries(Empty request,
          ServerCallContext context)
        {
            return await GetItems<CountryList, CountryDTO, Country>
               (_service.GetAllCountriesAsync);
        }
        //T - object with list of items
        //D - DTO from domain logic layer
        //I - item which is in list
        private async Task<T> GetItems<T, D, I>(Func<Task<ICollection<D>>> action) 
            where T: IItemList<I>, new()
        {
            var items = await action();
            var itemList = _mapper.Map<ICollection<D>, ICollection<I>>(items);
            var response = new T();
            response.Items.Add(itemList);
            return response;
        }
    }
}
