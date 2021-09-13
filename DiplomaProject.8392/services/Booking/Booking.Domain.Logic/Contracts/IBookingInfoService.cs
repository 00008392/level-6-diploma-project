using Booking.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.Contracts
{
    public interface IBookingInfoService
    {
        //can be viewed only by accommodation owner
        Task<ICollection<BookingRequestInfoDTO>> GetBookingRequestsForAccommodationAsync(long id);
        //booking requests made by user
        Task<ICollection<BookingRequestInfoDTO>> GetBookingRequestsForUserAsync(long id);
    }
}
