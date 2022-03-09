using FrontEndApp.Models;
using FrontEndApp.Models.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Services.Booking.Contracts
{
    //service that consumes booking api
    public interface IBookingService
    {
        Task<Response> CreateBookingAsync(CreateBooking booking,
            Action onSuccessAction = null, Action onErrorAction = null);
        Task<Response> DeleteBookingAsync(long id,
            Action onSuccessAction = null, Action onErrorAction = null);
        Task<Response> AcceptBookingAsync(long id,
            Action onSuccessAction = null, Action onErrorAction = null);
        Task<Response> RejectBookingAsync(long id,
         Action onSuccessAction = null, Action onErrorAction = null);
        Task<Response> CancelBookingAsync(long id,
         Action onSuccessAction = null, Action onErrorAction = null);
        Task<BookingResponse> GetBookingAsync(long id, Action onNotFoundAction = null);
        Task<ICollection<BookingResponse>> GetBookingsForGuestAsync(long guestId);
        Task<ICollection<BookingResponse>> GetBookingsForPostAsync(long postId);
    }
}
