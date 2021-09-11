using Booking.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.Contracts
{
    public interface IBookingRequestManipulationService
    {
        Task CreateBookingRequest(CreateBookingRequestDTO requestDTO);
        //can be deleted by the user who requested it until not accepted
        Task DeleteBookingRequest(long id);
        //can be accepted by the owner of accommodation being booked
        Task AcceptBookingRequest(long id);
        //can be cancelled by both owner and guest
        Task CancelBooking(CancelBookingRequestDTO requestDTO);
    }
}
