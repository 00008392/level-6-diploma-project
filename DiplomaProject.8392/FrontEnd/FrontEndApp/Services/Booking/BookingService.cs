using FrontEndApp.Models;
using FrontEndApp.Models.Booking;
using FrontEndApp.Services.Booking.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FrontEndApp.Services.Booking
{
    public class BookingService : IBookingService
    {
        private HttpClient _client;

        public BookingService(HttpClient client)
        {
            _client = client;
        }

        public async Task<Response> AcceptBookingAsync(long id, 
            Action onSuccessAction = null, Action onErrorAction = null)
        {
            var response = new Response();
            try
            {
                //call api
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Patch,
                    RequestUri = new Uri($"{_client.BaseAddress}api/bookings/accept/{id}")
                };
                var httpReply = await _client.SendAsync(request);
                //in case of success, login and set response to success
                if (httpReply.IsSuccessStatusCode)
                {
                    response.IsSuccess = true;
                    onSuccessAction?.Invoke();
                }
                //in case of error, parse error 
                else
                {
                    response.IsSuccess = false;
                    var errorMessage = httpReply.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<Response>(errorMessage);
                    onErrorAction?.Invoke();
                }
            }
            catch
            {
                response.IsSuccess = false;
            }
            return response;
        }

        public async Task<Response> CancelBookingAsync(long id, 
            Action onSuccessAction = null, Action onErrorAction = null)
        {
            var response = new Response();
            try
            {
                //call api
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Patch,
                    RequestUri = new Uri($"{_client.BaseAddress}api/bookings/cancel/{id}")
                };
                var httpReply = await _client.SendAsync(request);
                //in case of success, login and set response to success
                if (httpReply.IsSuccessStatusCode)
                {
                    response.IsSuccess = true;
                    onSuccessAction?.Invoke();
                }
                //in case of error, parse error 
                else
                {
                    response.IsSuccess = false;
                    var errorMessage = httpReply.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<Response>(errorMessage);
                    onErrorAction?.Invoke();
                }
            }
            catch
            {
                response.IsSuccess = false;
            }
            return response;
        }

        public async Task<Response> CreateBookingAsync(CreateBooking booking,
            Action onSuccessAction = null, Action onErrorAction = null)
        {
            var response = new Response();
            try
            {
                //call api
                var httpReply = await _client.PostAsJsonAsync("api/bookings", booking);
                //in case of success, login and set response to success
                if (httpReply.IsSuccessStatusCode)
                {
                    response.IsSuccess = true;
                    onSuccessAction?.Invoke();
                }
                //in case of error, parse error 
                else
                {
                    response.IsSuccess = false;
                    var errorMessage = httpReply.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<Response>(errorMessage);
                    onErrorAction?.Invoke();
                }
            }
            catch
            {
                response.IsSuccess = false;
            }
            return response;
        }

        public async Task<Response> DeleteBookingAsync(long id,
            Action onSuccessAction = null, Action onErrorAction = null)
        {
            var response = new Response();
            try
            {
                //call api
                var httpReply = await _client.DeleteAsync($"api/bookings/{id}");
                if (httpReply.IsSuccessStatusCode)
                {
                    response.IsSuccess = true;
                    onSuccessAction?.Invoke();
                }
                else
                {
                    //in case of error, parse error 
                    response.IsSuccess = false;
                    var errorMessage = httpReply.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<Response>(errorMessage);
                    onErrorAction?.Invoke();
                }
            }
            catch
            {
                response.IsSuccess = false;
            }
            return response;
        }

        public async Task<BookingResponse> GetBookingAsync(long id,
            Action onNotFoundAction = null)
        {
            try
            {
                var reply = await _client.GetAsync($"api/bookings/{id}");
                var user = new BookingResponse();
                if (reply.IsSuccessStatusCode)
                {
                    var responseStr = await reply.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<BookingResponse>(responseStr);
                }
                else
                {
                    user.NoItem = true;
                    onNotFoundAction?.Invoke();
                }
                return user;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ICollection<BookingResponse>> GetBookingsForGuestAsync(long guestId)
        {
            //call api
            try
            {
                var response = await _client.GetAsync($"api/bookings/guest/{guestId}");
                if (response.IsSuccessStatusCode)
                {
                    var responseStr = await response.Content.ReadAsStringAsync();
                    var bookings = JsonConvert.DeserializeObject<List<BookingResponse>>(responseStr);
                    return bookings?.Count == 0 ? null : bookings;
                }
            }
            catch
            {
            }
            return null;
        }

        public async Task<ICollection<BookingResponse>> GetBookingsForPostAsync(long postId)
        {
            //call api
            try
            {
                var response = await _client.GetAsync($"api/bookings/post/{postId}");
                if (response.IsSuccessStatusCode)
                {
                    var responseStr = await response.Content.ReadAsStringAsync();
                    var bookings = JsonConvert.DeserializeObject<List<BookingResponse>>(responseStr);
                    return bookings?.Count == 0 ? null : bookings;
                }
            }
            catch
            {
            }
            return null;
        }

        public async Task<Response> RejectBookingAsync(long id, Action onSuccessAction = null, Action onErrorAction = null)
        {
            var response = new Response();
            try
            {
                //call api
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Patch,
                    RequestUri = new Uri($"{_client.BaseAddress}api/bookings/reject/{id}")
                };
                var httpReply = await _client.SendAsync(request);
                //in case of success, login and set response to success
                if (httpReply.IsSuccessStatusCode)
                {
                    response.IsSuccess = true;
                    onSuccessAction?.Invoke();
                }
                //in case of error, parse error 
                else
                {
                    response.IsSuccess = false;
                    var errorMessage = httpReply.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<Response>(errorMessage);
                    onErrorAction?.Invoke();
                }
            }
            catch
            {
                response.IsSuccess = false;
            }
            return response;
        }
    }
}
