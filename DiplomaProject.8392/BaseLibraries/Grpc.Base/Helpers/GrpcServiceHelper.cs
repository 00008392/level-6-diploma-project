using AutoMapper;
using FluentValidation;
using Google.Protobuf.WellKnownTypes;
using Grpc.Base.Contracts;
using Grpc.Base.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grpc.Base.Helpers
{
    //this helper class contains methods shared by grpc services
    //since grpc methods for creation, modification, deletion and retreval of entities 
    //have the same structure in all microservices, they are moved to this class to reduce code duplication
    public static class GrpcServiceHelper
    {
        //this method adds list of DTO items to grpc reponse
        //TList - object with list of items
        //TDTO - DTO from domain logic layer
        //TItem - item which is in list
        public static TList MapItems<TList, TDTO, TItem>(
            IMapper mapper,
            ICollection<TDTO> DTOs) 
            where TList : IItemList<TItem>, new()
        {
            //map collection of dtos to collection of objects generated by grpc
            var itemList = mapper.Map<ICollection<TDTO>, ICollection<TItem>>(DTOs);
            //create grpc response that contains list of items
            var response = new TList();
            //add items to the list
            response.Items.Add(itemList);
            return response;
        }
        //this method handles retrieval of colleciton of items from domain logic layer
        public static async Task<TList> GetItemsAsync<TList, TDTO, TItem>(
            Func<Task<ICollection<TDTO>>> action,
            IMapper mapper)
           where TList : IItemList<TItem>, new()
        {
            //get items from domain logic service
            var items = await action();
            //map to response
            return MapItems<TList, TDTO, TItem>(mapper, items);
        }

        //this method handles creation of new entity and modification of existing entity
        //TDTO - dto that is accepted by domain logic service
        //TResponse - response returned by grpc method
        //TRequest - request accepted by grpc method
        public static async Task<TResponse> HandleCreateUpdateActionAsync<TDTO, TResponse, TRequest>
          (
            //action - create/update method from domain logic service
            Func<TDTO, Task> action,
            IMapper mapper,
            TRequest request) where TResponse : IResponse, new()
        {
            //map grpc request to dto
            var DTO = mapper.Map<TDTO>(request);
            return await HandleCreateUpdateActionAsync<TDTO, TResponse>(action, DTO);
        }
        public static async Task<TResponse> HandleCreateUpdateActionAsync<TDTO, TResponse>
         (
           //action - create/update method from domain logic service
           Func<TDTO, Task> action,
           TDTO DTO) where TResponse : IResponse, new()
        {
            var response = new TResponse();
            try
            {
                //try to perform domain logic action
                await action(DTO);
                //if successful, indicate it in response
                response.IsSuccess = true;
            }
            catch (ValidationException ex)
            {
                //if validation fails, add validation errors to the response
                response.HandleValidationException(ex);
            }
            catch (Exception ex)
            {
                //in case of other error, indicate it in response
                response.HandleException(ex);
            }
            return response;
        }
        //this method handles deletion of entity
        //TResponse - response returned by grpc method
        public static async Task<TResponse> HandleDeleteActionAsync<TResponse>
            (
            long id,
            //action - delete method from domain logic service
            Func<long, Task> action) where TResponse: IResponse, new()
        {
            var response = new TResponse();
            try
            {
                //try to perform domain logic action
                await action(id);
                //if successful, indicate it in response
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                //in case of error, indicate it in response
                response.HandleException(ex);
            }
            return response;
        }
        //this method handles retrieval of entity by id
        //TResponse - response returned by grpc method
        //TDTO - dto returned by domain logic service
        public static async Task<TResponse> HandleRetrievalByIdAsync<TResponse, TDTO>(
            long id,
            //action - retrieve by id method from domain logic service
            Func<long, Task<TDTO>> action,
            IMapper mapper)
            where TResponse: IItemInfoResponse, new()
        {
            //get item with indicated id
            var item = await action(id);
            if (item == null)
            {
                //if item does not exist, indicate it in response
                return new TResponse
                {
                    NoItem = true
                };
            }
            //if exists, map dto to grpc response
            var response = mapper.Map<TResponse>(item);
            return response;
        }
        //method that handles conversion from datetime to timestamp (format accepted by grpc)
        public static Timestamp ConvertDateTimeToTimeStamp(DateTime? time)
        {
            return time == null ? null : Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)time, DateTimeKind.Utc));
        }
    }
}
