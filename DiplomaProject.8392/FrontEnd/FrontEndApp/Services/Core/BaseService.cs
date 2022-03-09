using FrontEndApp.Models;
using FrontEndApp.Models.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FrontEndApp.Services.Core
{
    //base class for all services that consume from http client
    public abstract class BaseService
    {
        protected readonly HttpClient _client;

        protected BaseService(HttpClient client)
        {
            _client = client;
        }
        //method common for create methods
        protected async Task<Response> HandleCreateActionAsync<TRequest>(
            TRequest request,
            string url,
            Action onSuccessAction = null,
            Action onErrorAction = null)
        {
            //call base method
            return await HandleActionAsync(() => { return _client.PostAsJsonAsync(url, request); },
                onSuccessAction, onErrorAction);
        }
        //method common for update methods
        public async Task<Response> HandleUpdateActionAsync<TRequest>(
            TRequest request,
            string url,
            Action onSuccessAction = null,
            Action onErrorAction = null)
        {
            //call base method
            return await HandleActionAsync(() => { return _client.PutAsJsonAsync(url, request); },
                onSuccessAction, onErrorAction);
        }
        //method common for delete methods
        protected async Task<Response> HandleDeleteActionAsync(
            string url,
            Action onSuccessAction = null,
            Action onErrorAction = null)
        {
            //call base method
            return await HandleActionAsync(() => { return _client.DeleteAsync(url); },
                onSuccessAction, onErrorAction);
        }
        //method common for get single item methods
        protected async Task<TResponse> HandleSingleItemRetrievalAsync<TResponse>(
            string url,
            Action onNotFoundAction = null) where TResponse: IResponse, new()
        {
            try
            {
                //call api
                var reply = await _client.GetAsync(url);
                //prepare response
                var item = new TResponse();
                if (reply.IsSuccessStatusCode)
                {
                    //in case of success, parse response
                    var responseStr = await reply.Content.ReadAsStringAsync();
                    item = JsonConvert.DeserializeObject<TResponse>(responseStr);
                }
                //in case of error, indicate that item is not found
                //and invoke action that reacts to not found response
                else
                {
                    item.NoItem = true;
                    onNotFoundAction?.Invoke();
                }
                return item;
            }
            //in case of exception (can be thrown if response JSON conversion fails), return null
            catch
            {
                return default(TResponse);
            }
        }
        //method common for get multiple items methods
        protected async Task<ICollection<TResponse>> HandleMultipleItemsRetrievalAsync<TResponse>(
            string url)
        {
            try
            {
                //call api
                var response = await _client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    //in case of success, parse response
                    var responseStr = await response.Content.ReadAsStringAsync();
                    var items = JsonConvert.DeserializeObject<List<TResponse>>(responseStr);
                    //in case if collection of items is empty, return null
                    return items?.Count == 0 ? null : items;
                }
            }
            catch
            {
            }
            //in case of exception or error response return null
            return null;
        }
        //common method for handling actions
        protected async Task<Response> HandleActionAsync(
            Func<Task<HttpResponseMessage>> callApiAction,
            Action onSuccessAction = null,
            Action onErrorAction = null)
        {
            //prepare response
            var response = new Response();
            try
            {
                //execute api action
                var httpReply = await callApiAction();
                //in case of success, invoke on success action
                if (httpReply.IsSuccessStatusCode)
                {
                    response.IsSuccess = true;
                    onSuccessAction?.Invoke();
                }
                else
                {
                    //in case of error, parse error and invoke on error action
                    response.IsSuccess = false;
                    var errorMessage = httpReply.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<Response>(errorMessage);
                    onErrorAction?.Invoke();
                }
            }
            //in case of exception (can be thrown if JSON error response fails to be converted to Response)
            //indicate in response
            catch
            {
                response.IsSuccess = false;
            }
            return response;
        }
    }
}
