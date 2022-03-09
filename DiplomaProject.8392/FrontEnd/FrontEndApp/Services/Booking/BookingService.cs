using FrontEndApp.Models;
using FrontEndApp.Models.Booking;
using FrontEndApp.Services.Booking.Contracts;
using FrontEndApp.Services.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FrontEndApp.Services.Booking
{
    //service that consumes booking api
    public class BookingService :BaseService, IBookingService
    {
        public BookingService(HttpClient client)
            :base(client)
        {
        }
        //accept booking request
        public async Task<Response> AcceptBookingAsync(long id, 
            Action onSuccessAction = null, Action onErrorAction = null)
        {
            //call base method
            return await HandleActionAsync(() =>
            {
                return HandlePatchRequest($"api/bookings/accept/{id}");
            }, onSuccessAction, onErrorAction);
        }
        //cancel booking
        public async Task<Response> CancelBookingAsync(long id, 
            Action onSuccessAction = null, Action onErrorAction = null)
        {
            //call base method
            return await HandleActionAsync(() =>
            {
                return HandlePatchRequest($"api/bookings/cancel/{id}");
            }, onSuccessAction, onErrorAction);
        }
        //reject booking request
        public async Task<Response> RejectBookingAsync(long id, Action onSuccessAction = null, Action onErrorAction = null)
        {
            //call base method
            return await HandleActionAsync(() =>
            {
                return HandlePatchRequest($"api/bookings/reject/{id}");
            }, onSuccessAction, onErrorAction);
        }
        //create booking request
        public async Task<Response> CreateBookingAsync(CreateBooking booking,
            Action onSuccessAction = null, Action onErrorAction = null)
        {
            //call base service method for create
            return await HandleCreateActionAsync(booking, "api/bookings", onSuccessAction, onErrorAction);
        }
        //delete booking
        public async Task<Response> DeleteBookingAsync(long id,
            Action onSuccessAction = null, Action onErrorAction = null)
        {
            //call base service method for delete
            return await HandleDeleteActionAsync($"api/bookings/{id}", onSuccessAction, onErrorAction);
        }
        //get booking by id
        public async Task<BookingResponse> GetBookingAsync(long id,
            Action onNotFoundAction = null)
        {
            //call base service method for single item retrieval
            return await HandleSingleItemRetrievalAsync<BookingResponse>($"api/bookings/{id}", onNotFoundAction);
        }
        //get multiple bookings for guest
        public async Task<ICollection<BookingResponse>> GetBookingsForGuestAsync(long guestId)
        {
            //call base service method for multiple items retrieval
            return await HandleMultipleItemsRetrievalAsync<BookingResponse>($"api/bookings/guest/{guestId}");
        }
        //get multiple bookings for post
        public async Task<ICollection<BookingResponse>> GetBookingsForPostAsync(long postId)
        {
            //call base service method for multiple items retrieval
            return await HandleMultipleItemsRetrievalAsync<BookingResponse>($"api/bookings/post/{postId}");
        }
        //prepare patch request
        private Task<HttpResponseMessage> HandlePatchRequest(string url)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Patch,
                RequestUri = new Uri($"{_client.BaseAddress}{url}")
            };
            return _client.SendAsync(request);
        }
    }
}
